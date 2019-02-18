using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
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

        // GET: api/Livesearch/pa/5
        [HttpGet("{substring}/{qantityOfResult}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string substring, [FromRoute] int qantityOfResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var resultArr = await Task.Run(() =>_livesearchService.Find(substring, qantityOfResult));
                return Ok(resultArr);
            }
            catch
            {
                return NotFound(substring);
            }
        }
    }
}