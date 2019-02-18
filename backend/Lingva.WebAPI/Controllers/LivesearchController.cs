using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lingva.WebAPI.Dto;

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
        [HttpGet("{substring}/{qantityOfResult}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string substring, [FromRoute] int qantityOfResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable resultArr;

            try
            {
                resultArr = await Task.Run(() =>_livesearchService.Find(substring, qantityOfResult));
            }
            catch
            {
                return NotFound(substring);
            }

            return Ok(_mapper.Map<IEnumerable<WordViewDTO>>(resultArr));
        }
    }
}