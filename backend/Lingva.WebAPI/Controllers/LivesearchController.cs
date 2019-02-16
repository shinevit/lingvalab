using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivesearchController : ControllerBase
    {
        private readonly ILivesearchService _livesearchService;

        public LivesearchController(ILivesearchService livesearchService)
        {
            _livesearchService = livesearchService;
        }

        // GET: api/Livesearch/pa
        [HttpGet("{substring}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string substring)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable resultArr = await Task.Run(() => _livesearchService.Find(substring));
                return Ok(resultArr);
            }
            catch
            {
                return NotFound(substring);
            }
        }
    }
}