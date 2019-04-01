using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;

namespace Lingva.WebAPI.Controllers
{
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
        [HttpGet]
        public async Task<IActionResult> GetMoviesList()
        {
            var movies = _filmService.GetFilmList();
            return Ok(_mapper.Map<IEnumerable<FilmViewDTO>>(movies));
        }

        // GET: api/movie/5
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
        [HttpPost]
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

                await Task.Run(() => {
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

            await Task.Run(() => {
                newMovie = _filmService.GetFilm(movie.Id);
            });

            return Ok(_mapper.Map<FilmViewDTO>(newMovie));
        }

        // PUT: api/movie/5
        [HttpPut("{id}")]
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
