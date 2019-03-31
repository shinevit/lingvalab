using System;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslaterController : ControllerBase
    {
        private readonly Func<TranslaterServiceEnum, ITranslaterService> _translaterService;

        public TranslaterController(Func<TranslaterServiceEnum, ITranslaterService> translaterService)
        {
            _translaterService = translaterService;
        }

        // GET: api/Translater/0/time/en/ru
        [HttpGet("{serviceId}/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslation([FromRoute] TranslaterServiceEnum serviceId,
            [FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var strTranslation = await Task.Run(() =>
                    _translaterService(serviceId).Translate(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: api/Translater/list/0/time/en/ru
        [HttpGet("list/{serviceId}/{text}/{originalLanguage}/{translationLanguage}")]
        public async Task<IActionResult> GetTranslationVariants([FromRoute] TranslaterServiceEnum serviceId,
            [FromRoute] string text, [FromRoute] string originalLanguage, [FromRoute] string translationLanguage)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var strTranslation = await Task.Run(() =>
                    _translaterService(serviceId).GetTranslationVariants(text, originalLanguage, translationLanguage));
                return Ok(strTranslation);
            }
            catch
            {
                return NotFound(text);
            }
        }
    }
}