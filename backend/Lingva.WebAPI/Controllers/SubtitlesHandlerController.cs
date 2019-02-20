using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lingva.BusinessLayer.DTO;
using Lingva.BusinessLayer.Interfaces;
using Lingva.BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubtitlesHandlerController : ControllerBase
    {
        readonly ISubtitlesHandler _parserService;
        public SubtitlesHandlerController(ISubtitlesHandler parser)
        {
            _parserService = parser;
        }

        [HttpPost("UploadFile")]
        public SubtitlesRowDTO[] Post(IFormFile subtitlesFile)
        {
            var stream = subtitlesFile.OpenReadStream();

            var result = _parserService.Parse(stream, Encoding.UTF8);
            _parserService.AddSubtitles(result, subtitlesFile.FileName, 1);

            return result;
        }
    }
}
