using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Services;
using Lingva.DataAccessLayer.Exceptions;

namespace Lingva.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private IFilmService _FilmService;

        public MovieController(IFilmService FilmService)
        {
            _FilmService = FilmService;
        }

        [HttpGet("movies/{FilmID}")]
        public async Task<IActionResult> Get(int FilmID)
        {
            try
            {
                return Ok(await Task.Run(() => 
                    _FilmService.GetFilmInfo(FilmID)));
            }
            catch (LingvaException)
            {
                return BadRequest();
            }
        }
    }
}
