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
using NLog;

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
        /// <summary>
        /// Returns parsed words.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /ParserWords/{name}
        ///     { }
        ///
        /// </remarks>
        /// <param name="name">Word to parse</param>
        /// <returns>Parsed words</returns>
        /// <response code="200">Returns the parsed words</response>
        /// <response code="400">If model state is not valid</response> 
        /// <response code="404">If the exception handled</response> 
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetParserWord(string name)
        {
            _logger.Info($"GET Action: Attempt to get ParserWord record by \"{name}\"");

            if (!ModelState.IsValid)
            {
                _logger.Error("ModelState is not valid.");
                _logger.Error("400 StatusCode is generated.");

                return BadRequest(BaseStatusDto.CreateErrorDto("Bad request. Model state is not valid."));
            }

            try
            {
                _logger.Debug("Call ParserWordService for the request handling.");

                ParserWord word = _wordService.GetParserWord(name);

                if (word == null)
                {
                    _logger.Debug("The ParserWord record with Name = \"{name}\" is not found.");
                    _logger.Debug("404 StatusCode is generated.");

                    return NotFound(BaseStatusDto.CreateErrorDto(ERR_ID_NOT_FOUND + $"\"{name}\""));
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

                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }
        }

        // GET: api/parser
        /// <summary>
        /// Returns all parsed words.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /ParserWords
        ///     { }
        ///
        /// </remarks>
        /// <response code="200">Returns the parsed words</response>
        /// <response code="400">If the exception handled</response> 
        /// <response code="404">No founded words</response>
        /// <returns></returns>
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
                    return NotFound(BaseStatusDto.CreateErrorDto("There is no any record in the ParserWord table."));
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
        ///<summary>
        /// Updates dictionary record.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /ParserWords
        ///     {        
        ///        "Name" : "string"
        ///        "LanguageName" : "languageName"
        ///        "SubtitleRowId" : 132
        ///     }
        ///
        /// </remarks>
        /// <param name="word"></param>
        /// <response code="200">Returns OK if parser word updated</response>
        /// <response code="400">If model state is not valid</response>
        /// <response code="404">If the exception handled</response>
        /// <returns>Parsed words Dto</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutInsertOrUpdateParserWord([FromBody]ParserWordDTO word)
        {
            if (!ModelState.IsValid || word == null)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto("WordParserDTO request object is not correct."));
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
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }
        }

        // POST: api/parser/fromrow
        /// <summary>
        /// Creates parsed words from requested row.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /ParserWords
        ///     {
        ///        "Id" : 1
        ///        "Value" : "string"
        ///        "LanguageName" : "languageName"
        ///        "SubtitleId" : 135
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns OK if created</response>
        /// <response code="400">If model state is not valid</response> 
        /// <response code="404">If the exception handled</response> 
        /// <param name="rowDto">Row for parsing</param>
        /// <returns>Status and parsed words</returns>
        [HttpPost]
        [Route("fromrow")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddParserWordsFromRow([FromBody]SubtitleRowDTO rowDto)
        {
            if (!ModelState.IsValid || rowDto == null || string.IsNullOrEmpty(rowDto.Value))
            {
                return BadRequest(BaseStatusDto.CreateErrorDto("SubtitleRowDTO request object is not correct."));
            }
            try
            {
                SubtitleRow row = _mapper.Map<SubtitleRow>(rowDto);

                var dataWords = await Task.Run(() => _wordService.AddParserWordsFromRow(row));

                return Ok( new
                {
                    status = StatusCodes.Status201Created,
                    message = "The new records of the ParserWords table was successfully added.",
                    data = dataWords
                });
            }
            catch (Exception ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }
        }

        // POST: api/parser
        /// <summary>
        /// Creates parsed words.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /ParserWords
        ///     {
        ///         "Name" : "string"
        ///        "LanguageName" : "languageName"
        ///        "SubtitleRowId" : 132
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns OK if created</response>
        /// <response code="400">If the exception handled</response> 
        /// <param name="word">Word to create</param>
        /// <returns>Status and parsed words</returns>
        [HttpPost]
        [Route("word")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddParserWord([FromBody]ParserWordDTO word)
        {
            if (!ModelState.IsValid || word == null)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto("WordParserDTO request object is not correct."));
            }

            try
            {
                ParserWord parserWord = _mapper.Map<ParserWord>(word);

                await Task.Run(() => _wordService.AddParserWord(parserWord));

                word.CreateSuccess("The new record of the ParserWords table was successfully added.");

                return Ok(word);
            }
            catch (Exception ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto("WordParserDTO request object is not correct."));
            }
        }

        // DELETE: api/parser
        /// <summary>
        /// Deletes record from dictionary.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /ParserWords/{name}
        ///     { }
        ///
        /// </remarks>
        /// <response code="200">Returns OK if deleted</response>
        /// <response code="400">If exception is hendled</response> 
        /// <param name="name">Word to delete</param>
        /// <returns>Status of operation</returns>
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteParserWord(string name)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(name))
            {
                return BadRequest(BaseStatusDto.CreateErrorDto("Request is not correct."));
            }

            try
            {
                ParserWord parserWord = await Task.Run(() => _wordService.DeleteParserWord(name));

                ParserWordDTO parserWordDTO = _mapper.Map<ParserWordDTO>(parserWord);

                parserWordDTO.CreateSuccess($"The <{name}> ParserWord record is deleted.");

                return Ok(parserWordDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(BaseStatusDto.CreateErrorDto(ex.Message));
            }
        }
    }
}
