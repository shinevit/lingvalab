using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Lingva.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;

        public MovieController(IFilmService filmService, IMapper mapper)
        {
            _filmService = filmService;
            _mapper = mapper;
        }

        // GET: api/movie
        /// <summary>
        /// Returns list of avaliable movies
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: /movie    
        /// 
        /// </remarks>
        /// <returns>List of movies</returns>
        /// <response code="200">Returns OK and list of movies</response>
        /// <response code="400">If exception is hendled</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMoviesList()
        {
            var movies = _filmService.GetFilmList();
            return Ok(_mapper.Map<IEnumerable<FilmViewDTO>>(movies));
        }

        // GET: api/movie/5
        /// <summary>
        /// Returns chosen movie
        /// </summary>
        /// <remarks>
        ///     
        ///     GET: movie/{id}
        /// 
        /// </remarks>
        /// <param name="id">id of chosen movie</param>
        /// <returns>Chosen movie  Dto</returns>
        /// <response code="200">Returns OK if deleted</response>
        /// <response code="400">If model state is not valid</response> 
        /// <response code="404">If no movie for this id</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Film movie = _filmService.GetFilm(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FilmViewDTO>(movie));
        }

        // POST: api/movie
        /// <summary>
        /// Creates Movie
        /// </summary>
        /// <remarks>
        /// 
        ///     POST: movie/
        ///     
        /// Sample request:
        ///     {
        ///         "Title" : "title"
        ///         "Description" : "description"
        ///         "SubtitleId" : "subtitleId"
        ///         "Poster" : "poster"
        ///     }
        /// 
        /// </remarks>
        /// <param name="movieCreatingDTO">New movie info</param>
        /// <returns>Status and movie Dto</returns>
        /// <response code="200">Returns OK and movie Dto</response>
        /// <response code="400">If model state is not valid or exception hadled</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostGroup([FromBody] FilmCreatingDTO movieCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Film movie;

            try
            {
                movie = _mapper.Map<Film>(movieCreatingDTO);

                await Task.Run(() =>
                {
                    Film searchedMovie = _filmService.GetFilmByTitle(movie.Title);
                    if (searchedMovie == null)
                    {
                        _filmService.AddFilm(movie);
                    }
                    else
                    {
                        movie = searchedMovie;
                    }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            Film newMovie = new Film();

            await Task.Run(() =>
            {
                newMovie = _filmService.GetFilm(movie.Id);
            });

            return Ok(_mapper.Map<FilmViewDTO>(newMovie));
        }

        // PUT: api/movie/5
        /// <summary>
        /// Updates Movie info
        /// </summary>
        /// <remarks>
        /// 
        ///     PUT: movie/
        ///     
        /// Sample request:
        ///     {
        ///         "Title" : "title"
        ///         "Description" : "description"
        ///         "SubtitleId" : "subtitleId"
        ///         "Poster" : "poster"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">id of movie to update</param>
        /// <param name="movieCreatingDTO">New movie info</param>
        /// <returns>Status and movie Dto</returns>
        /// <response code="200">Returns OK and movie Dto</response>
        /// <response code="400">If model state is not valid or exception hadled</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMovie([FromRoute] int id, [FromBody] FilmCreatingDTO movieCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Film movie = _mapper.Map<Film>(movieCreatingDTO);
                await Task.Run(() => _filmService.UpdateFilm(id, movie));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // DELETE: api/movie/5
        /// <summary>
        /// Deletes chosen movie
        /// </summary>
        /// <remarks>
        /// 
        ///     DELETE: movie/{id}
        /// 
        /// </remarks>
        /// <param name="id">id of movie to delete</param>
        /// <returns>Status</returns>
        /// <response code="200">Returns OK</response>
        /// <response code="400">If model state is not valid or exception hadled</response> 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Task.Run(() => _filmService.DeleteFilm(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
