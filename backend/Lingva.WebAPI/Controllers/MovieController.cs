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
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        // GET: api/movie
        [HttpGet]
        public async Task<IActionResult> GetMoviesList()
        {
            var movies = _movieService.GetMovieList();
            return Ok(_mapper.Map<IEnumerable<MovieViewDTO>>(movies));
        }

        // GET: api/movie/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movie movie = _movieService.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieViewDTO>(movie));
        }

        // POST: api/movie
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] MovieCreatingDTO movieCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Movie movie;

            try
            {
                movie = _mapper.Map<Movie>(movieCreatingDTO);                

                await Task.Run(() => {
                    Movie searchedMovie = _movieService.GetMovieByTitle(movie.Title);
                    if (searchedMovie == null)
                    {
                        _movieService.AddMovie(movie);
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

            Movie newMovie = new Movie();

            await Task.Run(() => {
                newMovie = _movieService.GetMovie(movie.Id); ;
            });

            return Ok(_mapper.Map<MovieViewDTO>(newMovie));
        }

        // PUT: api/movie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie([FromRoute] int id, [FromBody] MovieCreatingDTO movieCreatingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Movie movie = _mapper.Map<Movie>(movieCreatingDTO);
                await Task.Run(() => _movieService.UpdateMovie(id, movie));
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
                await Task.Run(() => _movieService.DeleteMovie(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }   
}
