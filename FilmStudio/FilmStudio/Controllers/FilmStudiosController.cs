using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FilmStudio.Profiles.Models;
using AutoMapper;
using FilmStudio.Authentication;

namespace FilmStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmStudiosController : ControllerBase
    {
        private readonly IAppUserRepository _identityRepository;
        private readonly IMapper _mapper;

        public FilmStudiosController(IMapper mapper,
            IAppUserRepository identityRepository)
        {
            _mapper = mapper;
            _identityRepository = identityRepository;
        }


        // GET: api/filmsstudios
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _identityRepository.GetAll();
                return Ok(_mapper.Map<FilmStudioInfoModel[]>(result));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET: api/filmsstudios/username
        [HttpGet("{username}")]
        public IActionResult GetFilmStudio(string username)
        {
            try
            {
                var filmStudio =  _identityRepository.GetByUsername(username);

                if (filmStudio == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<FilmStudioInfoModel>(filmStudio)); 
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }


        
    }
}
