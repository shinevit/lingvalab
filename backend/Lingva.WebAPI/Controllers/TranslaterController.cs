using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslaterController : ControllerBase
    {
        private readonly Func<string, ITranslaterService> _translaterService;

        public TranslaterController(Func<string, ITranslaterService> translaterService)
        {
            _translaterService = translaterService;
        }

        // GET: api/Translater/g/time/en/ru
        [HttpGet("{serviceId}/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslation([FromRoute] string serviceId, [FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string strTranslation = await Task.Run(() => _translaterService(serviceId).Translate(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound("");
            }
        }

        // GET: api/Translater/list/g/time/en/ru
        [HttpGet("list/{serviceId}/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslationVariants([FromRoute] string serviceId, [FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string[] strTranslation = await Task.Run(() => _translaterService(serviceId).GetTranslationVariants(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound(text);
            }
        }
    }
}