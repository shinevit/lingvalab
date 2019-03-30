using System.Collections.Generic;
using System.IO;
using Lingva.BusinessLayer.Interfaces;
using Lingva.BusinessLayer.WordsSelector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtitlesAnalyzerController : ControllerBase
    {
        public List<Word> PostAnalyze(IFormFile upload)
        {
            if (upload == null) return null;

            string allText;
            using (var txt = new StreamReader(upload.OpenReadStream()))
            {
                allText = txt.ReadToEnd();
            }

            ICommonWord conection = new CommonWord();
            var analyzer = new Analyzer(conection);
            var words = analyzer.ParseToWords(allText);
            words = analyzer.RemoveSimpleWords(words);
            words = analyzer.RemoveNonExistent(words);

            //TODO: Save words to BD, and binding to same film

            return words;
        }
    }
}