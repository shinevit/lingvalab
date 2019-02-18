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
        private readonly ITranslaterGoogleService _translaterGoogleService;
        private readonly ITranslaterYandexService _translaterYandexService;

        public TranslaterController(ITranslaterGoogleService translaterGoogleService, ITranslaterYandexService translaterYandexService)
        {
            _translaterGoogleService = translaterGoogleService;
            _translaterYandexService = translaterYandexService;
        }

        // GET: api/Translater/g/time/en/ru
        [HttpGet("g/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslationGoogle([FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string strTranslation = await Task.Run(() => _translaterGoogleService.Translate(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound(text);
            }
        }

        // GET: api/Translater/g/list/time/en/ru
        [HttpGet("g/list/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslationVariantsGoogle([FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string[] strTranslation = await Task.Run(() => _translaterGoogleService.GetTranslationVariants(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound(text);
            }
        }

        // GET: api/Translater/y/time/en/ru
        [HttpGet("y/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslationYandex([FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string strTranslation = await Task.Run(() => _translaterYandexService.Translate(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound(text);
            }
        }

        // GET: api/Translater/y/list/time/en/ru
        [HttpGet("y/list/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslationVariantsYandex([FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string[] strTranslation = await Task.Run(() => _translaterYandexService.GetTranslationVariants(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound(text);
            }
        }
    }
}