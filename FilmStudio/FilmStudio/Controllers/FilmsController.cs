using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmStudio.Authentication;
using FilmStudio.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmStudio.Entities;
using FilmStudio.Profiles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace FilmStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IRentalRecordRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public FilmsController(IRentalRecordRepository repository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        // GET: api/Films
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var films = await _repository.GetAllFilmsAsync();

            if (!User.Identity.IsAuthenticated)
            {
                return Ok(_mapper.Map<IEnumerable<FilmModelForNonauthorized>>(films));
            }
            return Ok(_mapper.Map<IEnumerable<FilmModel>>(films));
        }

        // GET: api/Films/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var film = await _repository.GetFilmAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Ok(_mapper.Map<FilmModelForNonauthorized>(film));
            }
            return Ok(_mapper.Map<FilmModel>(film));
        }

        // PUT: api/Films/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FilmModel>> EditFilmInfo(int id, FilmUpdateModel model)
        {
            try
            {
                var oldFilm = await _repository.GetFilmAsync(id);
                if (oldFilm == null) return NotFound($"Could not find film with id of {id}");

                _mapper.Map(model, oldFilm);

                if (oldFilm.TotalNumberOfCopies > oldFilm.NumberOfRentedCopies)
                {
                    _repository.Update(oldFilm);

                    if (await _repository.SaveChangesAsync())
                    {
                        return _mapper.Map<FilmModel>(oldFilm);
                    }
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Total number of a film should be larger than the number of rented copies.");
        }

        //POST: api/Films
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FilmModel>> PostFilm(FilmModel model)
        {
            try
            {
                var existing = await _repository.GetFilmAsync(model.FilmId);
                if (existing != null)
                {
                    return BadRequest("This film is already in the database");
                }

                var location = _linkGenerator.GetPathByAction("Get", "Films",
                    new { id = model.FilmId });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current id");
                }

                var newFilm = _mapper.Map<Film>(model);
                _repository.Add(newFilm);

                if (await _repository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<FilmModel>(newFilm));
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        // DELETE: api/Films/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _repository.GetFilmAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            if (film.NumberOfRentedCopies > 0)
            {
                return BadRequest("You cannot remove a film when there are rented copies");
            }

            _repository.Delete(film);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
