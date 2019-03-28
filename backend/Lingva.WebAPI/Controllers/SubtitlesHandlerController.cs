using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.DTO;
using Lingva.BusinessLayer.Services;
using Lingva.DataAccessLayer.Entities;
using Lingva.WebAPI.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/subtitle")]
    [ApiController]
    public class SubtitlesHandlerController : ControllerBase
    {
        private readonly ISubtitlesHandlerService _subtitleService;

        private readonly IParserWordService _wordService;
        private readonly IMapper _mapper;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public SubtitlesHandlerController(ISubtitlesHandlerService parser)
        {
            _subtitleService = parser;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubtitleById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseClassDTO
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Id of subtitle is incorrect."
                }); 
            }

            DictionaryRecord dictionaryRecord = _dictionaryService.GetDictionaryRecord(id);

            if (dictionaryRecord == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DictionaryRecordViewDTO>(dictionaryRecord));
        }

        [HttpPost]
        public IActionResult Post([FromBody]SubtitleDTO subtitleDto)
        {
            if (!ModelState.IsValid || subtitleDto == null)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "WordParserDTO request object is not correct."
                });
            }

            try
            {
                Subtitle subtitle = _mapper.Map<Subtitle>(subtitleDto);

                IEnumerable<SubtitleRow>  rows =_subtitleService.ParseSubtitle(subtitle);

                if (rows == null)
                {
                    return BadRequest(new BaseClassDTO
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "There are not any rows from parsing subtitle by the ParserWordService."
                    }); 
                }

                IEnumerable<SubtitlesRowDTO> rowDTOs = _mapper.Map<IEnumerable<SubtitlesRowDTO>>(rows);

                return  Ok(new BaseClassDTO
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Subtitle parsing operation is successful."
                });
            }
            catch (Exception ex)
            {
                _logger.Debug($"{ex.GetType()} exception is generated.");
                _logger.Debug($"{ex.Message}");

                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = ex.Message
                });
            }
        }
    }
}
