﻿using System;
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

namespace Lingva.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtitlesAnalyzerController : ControllerBase
    {
        public List<Word> PostAnalyze(HttpPostedFileBase upload)
        {
            if (upload == null)
            {
                return null;
            }

            string allText;
            using (StreamReader txt = new StreamReader(upload))
            {
                allText = txt.ReadToEnd();
            }

            Analyzer analyzer = new Analyzer( );//TODO: add ICommonWord
            List<Word> words = analyzer.ParseToWords(allText);
            words = analyzer.RemoveSimpleWords(words);
            words = analyzer.RemoveNonExistent(words);

            //TODO: Save words to BD, and binding to same film

            return words;
        }
    }
}