using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.DTO;
using Lingva.BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/subtitle")]
    [ApiController]
    public class SubtitlesHandlerController : ControllerBase
    {
        readonly ISubtitlesHandlerService _parserService;
        public SubtitlesHandlerController(ISubtitlesHandlerService parser)
        {
            _parserService = parser;
        }

        //POST: api/subtitle/1
        [HttpPost("{id?}")]
        public SubtitlesRowDTO[] Post([FromForm]IFormFile subtitlesFile, int? filmId)
        {
            var stream = subtitlesFile.OpenReadStream();

            var result = _parserService.Parse(stream);

            int? subtitleFilmId = filmId ?? 1; 

            _parserService.AddSubtitles(result, subtitlesFile.FileName, subtitleFilmId);

            stream.Close();

            return result;
        }
    }
}
