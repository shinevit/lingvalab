using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AutoMapper;
using Lingva.DataAccessLayer.Entities;
using Lingva.BusinessLayer.Contracts;
using Lingva.WebAPI.Dto;
using Microsoft.AspNetCore.Http;
using System.IO;
using Lingva.BusinessLayer.WordsSelector;
using Lingva.BusinessLayer.Interfaces;

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtitlesAnalyzerController : ControllerBase
    {
        public List<BusinessLayer.WordsSelector.Word> PostAnalyze(IFormFile upload)
        {
            if (upload == null)
            {
                return null;
            }

            string allText; 
            using (StreamReader txt = new StreamReader(upload.OpenReadStream()))
            {
                allText = txt.ReadToEnd();
            }

            ICommonWord conection = new CommonWord();
            Analyzer analyzer = new Analyzer(conection);
            List<BusinessLayer.WordsSelector.Word> words = analyzer.ParseToWords(allText);
            words = analyzer.RemoveSimpleWords(words);
            words = analyzer.RemoveNonExistent(words);

            //TODO: Save words to BD, and binding to same film

            return words;
        }
    }
}