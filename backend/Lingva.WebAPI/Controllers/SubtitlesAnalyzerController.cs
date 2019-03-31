using System.Collections.Generic;
using System.IO;
using Lingva.BusinessLayer.Contracts;
using Lingva.BusinessLayer.WordsSelector;
using Microsoft.AspNetCore.Http;
using Word = Lingva.BusinessLayer.WordsSelector.Word;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtitlesAnalyzerController : ControllerBase
    {
        public List<Word> PostAnalyze(IFormFile upload)
        {
            if (upload == null)
            {
                return null;
            }

            string allText;
            using (var txt = new StreamReader(upload.OpenReadStream()))
            {
                allText = txt.ReadToEnd();
            }

            ICommonWord conection = new CommonWord();
            Analyzer analyzer = new BusinessLayer.WordsSelector.Analyzer(conection);
            List<Word> words = analyzer.ParseToWords(allText);
            words = analyzer.RemoveSimpleWords(words);
            words = analyzer.RemoveNonExistent(words);

            return words;           
        }
    }
}