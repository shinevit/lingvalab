using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lingva.WebAPI.Dto;
using Microsoft.AspNetCore.Http;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivesearchController : ControllerBase
    {
        private readonly ILivesearchService _livesearchService;
        private readonly IMapper _mapper;

        public LivesearchController(ILivesearchService livesearchService, IMapper mapper)
        {
            _livesearchService = livesearchService;
            _mapper = mapper;
        }

        // GET: api/Livesearch/pa/5
        /// <summary>
        ///     Livesearch by chosen substring
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: Livesearch/{substring}/{qantityOfResult}
        /// 
        /// </remarks>
        /// <param name="substring">Searched substring</param>
        /// <param name="qantityOfResult">Wished qantity</param>
        /// <returns>Search results</returns>
        /// <response code="200">Returns OK and search results</response>
        /// <response code="400">If model state is not valid</response> 
        /// <response code="404">If nothing was found</response>
        [HttpGet("{substring}/{qantityOfResult}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTranslation([FromRoute] string substring, [FromRoute] int qantityOfResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable resultArr;

            try
            {
                resultArr = await Task.Run(() => _livesearchService.Find(substring, qantityOfResult));
            }
            catch
            {
                return NotFound(substring);
            }

            return Ok(_mapper.Map<IEnumerable<WordViewDTO>>(resultArr));
        }
    }
}