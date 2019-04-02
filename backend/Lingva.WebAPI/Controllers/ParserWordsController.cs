﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.WebAPI.Dto;
using System.Text;
using NLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/parser")]
    [ApiController]
    public class ParserWordsController : ControllerBase
    {
        private readonly IParserWordService _wordService;
        private readonly IMapper _mapper;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private const string ERR_ID_NOT_FOUND = "There is no ParserWord object with Name = ";

        public ParserWordsController(IParserWordService wordService, IMapper mapper)
        {
            _wordService = wordService;
            _mapper = mapper;
        }

        // GET: api/parser/car
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetParserWord(string name)
        {
            _logger.Info($"GET Action: Attempt to get ParserWord record by \"{name}\"");

            if (!ModelState.IsValid)
            {
                _logger.Error("ModelState is not valid.");
                _logger.Error("400 StatusCode is generated.");

                return BadRequest(BaseStatusDTO.CreateErrorDto("Bad request. Model state is not valid."));
            }

            try
            {
                _logger.Debug("Call ParserWordService for the request handling.");

                ParserWord word = _wordService.GetParserWord(name);

                if (word == null)
                {
                    _logger.Debug("The ParserWord record with Name = \"{name}\" is not found.");
                    _logger.Debug("404 StatusCode is generated.");

                    return NotFound(BaseStatusDTO.CreateErrorDto(ERR_ID_NOT_FOUND + $"\"{name}\""));
                }

                _logger.Debug("The ParserWord record with Name = \"{name}\" is found.");
                _logger.Info("Database record from ParserWords is safely returned to the client side.");
                _logger.Debug("200 StatusCode is generated.");

                ParserWordDTO wordDTO = _mapper.Map<ParserWordDTO>(word);
                wordDTO.CreateSuccess("GET request succeeds.");

                return Ok(wordDTO); 
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.GetType()} exception is generated.");
                _logger.Error($"{ex.Message}");

                return BadRequest(BaseStatusDTO.CreateErrorDto(ex.Message));
            }
        }

        // GET: api/parser
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ParserWord>>> GetAllParserWords()
        {
            try
            {
                var parserWords = _wordService.GetAllParserWords();

                if (parserWords == null)
                {
                    return NotFound(BaseStatusDTO.CreateErrorDto("There is no any record in the ParserWord table."));
                }

                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "GET request succeeds.",
                    data = _mapper.Map<IEnumerable<ParserWordDTO>>(parserWords)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message
                });
            }
        }

        // PUT: api/parser
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutInsertOrUpdateParserWord([FromBody]ParserWordDTO word)
        {
            if (!ModelState.IsValid || word == null)
            {
                return BadRequest(BaseStatusDTO.CreateErrorDto("WordParserDTO request object is not correct."));
            }

            try
            {
                ParserWord parserWord = _mapper.Map<ParserWord>(word);

                await Task.Run(() => _wordService.InsertOrUpdateParserWord(parserWord));

                ParserWordDTO parserWordDto = _mapper.Map<ParserWordDTO>(parserWord);

                return Ok(parserWordDto);
            }
            catch (Exception ex)
            {
                return BadRequest(BaseStatusDTO.CreateErrorDto(ex.Message));
            }
        }

        // POST: api/parser/fromrow
        [HttpPost]
        [Route("fromrow")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddParserWordsFromRow([FromBody]SubtitleRowDTO rowDto)
        {
            if (!ModelState.IsValid || rowDto == null || string.IsNullOrEmpty(rowDto.Value))
            {
                return BadRequest(BaseStatusDTO.CreateErrorDto("SubtitleRowDTO request object is not correct."));
            }
//------------------------------------------------------------------------------------------------------------------------
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
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message
                });
            }
        }

        // POST: api/parser
        [HttpPost]
        [Route("word")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message
                });
            }
        }

        // DELETE: api/parser
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                    status = StatusCodes.Status200OK,
                    message = $"The <{name}> ParserWord record is deleted.",
                    data = parserWordDTO
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message
                });
            }
        }
    }
}