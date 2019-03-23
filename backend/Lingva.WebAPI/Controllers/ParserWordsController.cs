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
        private readonly IParserWordService _wordService;
        private readonly IMapper _mapper;

        private const string ERR_ID_NOT_FOUND = "There is no ParserWord object with Name = ";

        public ParserWordsController(IParserWordService wordService, IMapper mapper)
        {
            _wordService = wordService;
            _mapper = mapper;
        }

        // GET: api/parser/car
        [HttpGet("{name}")]
        public async Task<IActionResult> GetParserWord(string name) 
        {
            if (!ModelState.IsValid)
            {
                return  BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Bad request. Model state is not valid."
                });
            }

            ParserWord word = _wordService.GetParserWord(name);
            
            if (word == null)
            {
                return NotFound(ERR_ID_NOT_FOUND + $"\"{name}>\"");
            }

            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "GET request succeeds.",
                data = _mapper.Map<WordParserDTO>(word)
            });
        }

        // GET: api/parser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParserWord>>> GetAllParserWords()
        {
            var parserWords = _wordService.GetAllParserWords();

            if(!parserWords.Any())
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status204NoContent,
                    message = "There is no any record in the ParserWord table."
                });
            }

            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "GET request succeeds.",
                data = _mapper.Map<IEnumerable<WordParserDTO>>(parserWords)
            });
        }

        // POST: api/parser/addword
        [HttpPost("addword")]
        public async Task<IActionResult> PostCreateParserWord([FromBody]WordParserDTO word)
        {
            if (!ModelState.IsValid || word == null)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Request object is not correct."
                });
            }

            try
            {
                ParserWord parserWord = _mapper.Map<ParserWord>(word);

                if (await Task.Run(() => _wordService.AddWord(parserWord)))
                {
                    return Ok( new
                    {
                        status = StatusCodes.Status201Created,
                        message = "The new record of the ParserWords table was successfully added.",
                        data = word
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest( new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message
                });
            }

            return BadRequest();
        }

        // PUT: api/parser/en/3
        [HttpPost("{lang?}/{subtId?}")]
        public async Task<IActionResult> PostCreateParserWordsFromPhrase(string lang, int? subtId, [FromBody]string phrase)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(phrase))
            {
                return BadRequest(); 
            }

            try
            {
                if (await Task.Run(() => _wordService.AddParserWordsFromPhrase(phrase, lang, subtId)))
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
