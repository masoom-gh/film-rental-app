using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FilmStudio.Authentication;
using FilmStudio.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FilmStudio.Entities;
using FilmStudio.Profiles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace FilmStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalRecordsController : ControllerBase
    {
        private readonly IRentalRecordRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalRecordsController(IRentalRecordRepository repository, 
            IMapper mapper, LinkGenerator linkGenerator, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _userManager = userManager;
        }

        // GET: api/RentalRecords
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RentalRecordModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllRentalRecordsAsync();
                return _mapper.Map<RentalRecordModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            
        }

        // GET: api/RentalRecords/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RentalRecordModel>> Get(int id)
        {
            try
            {
                var result = await _repository.GetRentalRecordAsync(id);

                if (result == null) return NotFound();

                return _mapper.Map<RentalRecordModel>(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            
        }

        // GET: api/RentalRecords
        [HttpGet("search")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<RentalRecordModel[]>> GetByFilmStudio(bool includeOrderItem = false)
        {
            try
            {
                var currentFilmStudio = await _userManager.FindByNameAsync(User.Identity.Name);
                var filmstudioUsername = currentFilmStudio.UserName;

                var results = await _repository.GetAllRentalRecordsByFilmStudio(filmstudioUsername,includeOrderItem);

                if (!results.Any()) return NotFound();

                return _mapper.Map<RentalRecordModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

      
        // POST: api/RentalRecords
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<RentalRecordModel>> Post(RentalRecordModel model)
        {
            try
            {
                var location = _linkGenerator.GetPathByAction("Get", "RentalRecords",
                    new { orderId = model.OrderId });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current rental order ID");
                }

                if (ModelState.IsValid)
                {
                    var newRentalRecord = _mapper.Map<RentalRecordModel, RentalRecord>(model);

                    if (newRentalRecord.RentalDate == DateTime.MinValue)
                    {
                        newRentalRecord.RentalDate = DateTime.Now;
                    }

                    var currentFilmStudio = await _userManager.FindByNameAsync(User.Identity.Name);
                    newRentalRecord.FilmStudio = currentFilmStudio;

                    var film = await _repository.GetFilmAsync(model.Film.FilmId);
                    if (model.Film != null)
                    {
                        if (film.NumberOfRentedCopies < film.TotalNumberOfCopies)
                        {
                            film.NumberOfRentedCopies++;
                            newRentalRecord.Film = film;
                        }
                        else
                        {
                            return BadRequest("This film is not available");
                        }
                        
                    }
                    
                    _repository.Add(newRentalRecord);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created(location, _mapper.Map<RentalRecord, RentalRecordModel>(newRentalRecord));
                    }
                }
   
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed to create the rental order");
        }



        // DELETE: api/RentalRecords/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldRentalRecord = await _repository.GetRentalRecordAsync(id);
                if (oldRentalRecord == null) return NotFound();

                var filmId = oldRentalRecord.Film.FilmId;
                var film = await _repository.GetFilmAsync(filmId);
                if (film.NumberOfRentedCopies > 0)
                {
                    film.NumberOfRentedCopies--;
                }

                _repository.Delete(oldRentalRecord);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed to delete the rental order");


        }

        
    }
}
