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
using System.Text;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/parser")]
    [ApiController]
    public class ParserWordsController : ControllerBase
    {
        private readonly IParserWordService _wordService;
        private readonly IMapper _mapper;
        private readonly char[] _separators = new char[] { '\'', 'n', 'r' };


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
                return BadRequest(new
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
                data = _mapper.Map<ParserWordDTO>(word)
            });
        }

        // GET: api/parser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParserWord>>> GetAllParserWords()
        {
            var parserWords = _wordService.GetAllParserWords();

            if (!parserWords.Any())
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
                data = _mapper.Map<IEnumerable<ParserWordDTO>>(parserWords)
            });
        }

        // PUT: api/parser
        [HttpPut]
        public async Task<IActionResult> PutInsertOrUpdateParserWord([FromBody]ParserWordDTO word)
        {
            if (!ModelState.IsValid || word == null)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "WordParserDTO request object is not correct."
                });
            }

            try
            {
                ParserWord parserWord = _mapper.Map<ParserWord>(word);

                await Task.Run(() => _wordService.InsertOrUpdateParserWord(parserWord));

                return Ok(new
                {
                    status = StatusCodes.Status201Created,
                    message = "The new record of the ParserWords table was successfully inserted.",
                    data = word
                });
            }
            catch (Exception ex)
            {
                string innerMessage = string.Empty;

                if (ex.InnerException != null)
                {
                    innerMessage = ex.InnerException.Message;
                }

                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message + "\nInnerException:" + innerMessage
                });
            }
        }

        // POST: api/parser/fromrow
        [HttpPost]
        [Route("fromrow")]
        public async Task<IActionResult> AddParserWordsFromRow([FromBody]SubtitleRowDTO rowDto)
        {
            if (!ModelState.IsValid || rowDto == null || string.IsNullOrEmpty(rowDto.Value))
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "SubtitleRowDTO request object is not correct."
                });
            }

            try
            {
                SubtitleRow row = _mapper.Map<SubtitleRow>(rowDto);

                var dataWords = await Task.Run(() => _wordService.AddParserWordsFromRow(row));

                return Ok(new
                {
                    status = StatusCodes.Status201Created,
                    message = "The new records of the ParserWords table was successfully added.",
                    data = dataWords
                });
            }
            catch (Exception ex)
            {
                string innerMessage = string.Empty;

                if (ex.InnerException != null)
                {
                    innerMessage = ex.InnerException.Message;
                }

                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message + "\nInnerException:" + innerMessage
                });
            }
        }

        // POST: api/parser
        [HttpPost]
        [Route("word")]
        public async Task<IActionResult> AddParserWord([FromBody]ParserWordDTO word)
        {
            if (!ModelState.IsValid || word == null)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "WordParserDTO request object is not correct."
                });
            }

            try
            {
                ParserWord parserWord = _mapper.Map<ParserWord>(word);

                await Task.Run(() => _wordService.AddParserWord(parserWord));

                return Ok(new
                {
                    status = StatusCodes.Status201Created,
                    message = "The new record of the ParserWords table was successfully added.",
                    data = word
                });
            }
            catch (Exception ex)
            {
                string innerMessage = string.Empty;

                if (ex.InnerException != null)
                {
                    innerMessage = ex.InnerException.Message;
                }

                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message + "\nInnerException:" + innerMessage
                });
            }
        }

        // POST: api/parser
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteParserWord(string name)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(name))
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Request is not correct."
                });
            }

            try
            {
                ParserWord parserWord = await Task.Run(() => _wordService.DeleteParserWord(name) );

                ParserWordDTO parserWordDTO = _mapper.Map<ParserWordDTO>(parserWord);

                return Ok(new
                {
                    status = StatusCodes.Status201Created,
                    message = $"The <{name}> ParserWord record is deleted.",
                    data = parserWordDTO
                });
            }
            catch (Exception ex)
            {
                StringBuilder resultMessage = new StringBuilder();

                resultMessage.Append(ex.Message);

                if (ex.InnerException != null)
                {
                    resultMessage.AppendLine(ex.InnerException.Message);
                }

                string result = resultMessage.ToString().Trim(_separators);

                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = result
            });
            }
        }
    }
}
