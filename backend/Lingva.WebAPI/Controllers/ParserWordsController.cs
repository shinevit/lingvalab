using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using System.ComponentModel.Design;
using AutoMapper;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/parser")]
    [ApiController]
    public class ParserWordsController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly IMapper _mapper;

        private const string ERR_ID_NOT_FOUND = "There is no ParserWord object with id = ";

        public ParserWordsController(IWordService wordService, IMapper mapper)
        {
            _wordService = wordService;
            _mapper = mapper;
        }

        // GET: api/parser/car
        [HttpGet("{name}")]
        public async Task<IActionResult> GetWord(string name) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ParserWord word = _wordService.GetParserWordByName(name);
            
            if (word == null)
            {
                return NotFound(ERR_ID_NOT_FOUND + $"{name}");
            }

            return Ok(_mapper.Map<WordDTO>(word));
        }

        // GET: api/parser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParserWord>>> GetParserWords()
        {
            var parserWords = _wordService.GetAll();

            return Ok(_mapper.Map<IEnumerable<WordDTO>>(parserWords));
        }

        // PUT: api/parser/en/3
        [HttpPut("{lang?}/{subtId?}")]
        public async Task<IActionResult> PutWordFromPhrase(string lang, int? subtId, [FromBody]string phrase)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(phrase))
            {
                return BadRequest(); 
            }

            try
            {
                if (await Task.Run(() => _wordService.AddWordFromPhrase(phrase, lang, subtId)))
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
            return BadRequest();
        }
    }
}
