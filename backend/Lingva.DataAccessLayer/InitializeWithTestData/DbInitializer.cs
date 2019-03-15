using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lingva.DataAccessLayer.InitializeWithTestData
{
    public static class DbInitializer
    {
        public static void InitializeParserWords(IApplicationBuilder applicationBuilder)
        {
            DictionaryContext context = applicationBuilder.ApplicationServices.GetRequiredService<DictionaryContext>();

            if (context.ParserWords.Any())
            {
                return;                       
            }

            var parserWords = new ParserWord[]
            {
                new ParserWord { Name = "car"},
                new ParserWord { Name = "time"},
                new ParserWord { Name = "beatiful"},
                new ParserWord { Name = "flower"},
                new ParserWord { Name = "clean"},
                new ParserWord { Name = "rain"},
                new ParserWord { Name = "table"},
                new ParserWord { Name = "strong"},
            };

            foreach (ParserWord word in parserWords)
            {
                context.ParserWords.Add(word);
            }

            context.SaveChanges();
        }
    }
}
