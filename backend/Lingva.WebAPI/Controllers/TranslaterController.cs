using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Services;
using Lingva.BusinessLayer.Translater;
using Lingva.DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslaterController : ControllerBase
    {
        private readonly ITranslaterService _translaterService;

        public TranslaterController(ITranslaterService translaterService)
        {
            _translaterService = translaterService;           
        }

        // GET: api/Translater/paper/en/ru
        [HttpGet("{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
                string strTranslation = await Task.Run(() => _translaterService.Translate(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound(text);
            }
        }
    }
}