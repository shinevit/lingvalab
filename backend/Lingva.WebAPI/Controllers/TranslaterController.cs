using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Lingva.BusinessLayer.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslaterController : ControllerBase
    {
        private readonly Func<TranslaterServices, ITranslaterService> _translaterService;

        public TranslaterController(Func<TranslaterServices, ITranslaterService> translaterService)
        {
            _translaterService = translaterService;
        }

        // GET: api/Translater/0/time/en/ru
        /// <summary>
        /// Translates words withing choosen services.
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: {serviceId}/{text}/{originalLanguage}/{translationLanguage}
        /// 
        /// Returns one translation.
        /// 
        /// </remarks>
        /// <param name="serviceId">Id of service to translate</param>
        /// <param name="text">Text to translate</param>
        /// <param name="originalLanguage">Original language</param>
        /// <param name="translationLanguage">TranslationLanguage</param>
        /// <returns>Translation of text</returns>
        /// <response code="200">Returns status and translation</response>
        /// <response code="400">If model state is not valid</response> 
        /// <response code="404">If the exception is handled</response> 
        [HttpGet("{serviceId}/{text}/{originalLanguage}/{translationLanguage}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTranslation([FromRoute] TranslaterServices serviceId, [FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
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
                return NotFound();
            }
        }

        // GET: api/Translater/list/0/time/en/ru
        /// <summary>
        /// Translates words withing choosen services. Returns several variants of translation.
        /// </summary>
        /// <remarks>
        /// 
        ///     GET: list/{serviceId}/{text}/{originalLanguage}/{translationLanguage}
        /// 
        /// Returns list of translations.
        /// 
        /// </remarks>
        /// <param name="serviceId">Id of service to translate</param>
        /// <param name="text">Text to translate</param>
        /// <param name="originalLanguage">Original language</param>
        /// <param name="translationLanguage">TranslationLanguage</param>
        /// <returns>List of translations</returns>
        /// <response code="200">Returns status and translation</response>
        /// <response code="400">If model state is not valid</response> 
        /// <response code="404">If the exception is handled</response> 
        [HttpGet("list/{serviceId}/{text}/{originalLanguage}/{translationLanguage}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTranslationVariants([FromRoute] TranslaterServices serviceId, [FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
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