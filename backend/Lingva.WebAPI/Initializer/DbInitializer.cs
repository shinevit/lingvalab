using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Repositories.Lingva.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.WebAPI.Initializer
{
    public static class DbInitializer
    {
        private const string INIT_PARSER_WORDS_ERR = "Initialization is not performed because the database has already been initialized.";
        private const string DEINIT_PARSER_WORDS_ERR = "Deinitialization is not performed because the base is not initialized.";

        public static SubtitleRow[] _subtitleRows = new SubtitleRow[]
        {
            new SubtitleRow { Value = "nothing occurred that seemed important.", LanguageName = "en"},
            new SubtitleRow { Value = "we have accomplished all we set out to do.", LanguageName = "en"},
            new SubtitleRow { Value = "the shadow sometimes falls on the moon", LanguageName = "en"}
        };

        public static ParserWord[] _parserWords = new ParserWord[]
            {
                new ParserWord { Name = "cat", LanguageName = "en", SubtitleRowId = 1},
                new ParserWord { Name = "spring", LanguageName = "en", SubtitleRowId = 1},
                new ParserWord { Name = "beatiful", LanguageName = "en", SubtitleRowId = 2},
                new ParserWord { Name = "flower", LanguageName = "en", SubtitleRowId = 1},
                new ParserWord { Name = "clean", LanguageName = "en", SubtitleRowId = 2},
                new ParserWord { Name = "rain", LanguageName = "en", SubtitleRowId = 1},
                new ParserWord { Name = "table", LanguageName = "en", SubtitleRowId = 2},
                new ParserWord { Name = "strong", LanguageName = "en", SubtitleRowId = 1},
                new ParserWord { Name = "fun", LanguageName = "en", SubtitleRowId = 1},
            };

        public static void InitializeParserWords(IUnitOfWorkParser unitOfWork, bool fillOnlyEmptyDB = false)
        {
            if (fillOnlyEmptyDB && unitOfWork.ParserWords.Any())
            {
                return;
            }
            
            foreach (ParserWord word in _parserWords)
            {
                unitOfWork.ParserWords.InsertOrUpdate(word);
            }

            unitOfWork.Save();
        }

        public static void InitializeSubtitleRows(IUnitOfWorkParser unitOfWork, bool fillOnlyEmptyDB = false)
        {
            if (fillOnlyEmptyDB && unitOfWork.SubtitleRows.Any())
            {
                return;
            }

            foreach (SubtitleRow row in _subtitleRows)
            {
                unitOfWork.SubtitleRows.InsertOrUpdate(row);
            }

            unitOfWork.Save();
        }
        public static void DeinitializeParserWords(IUnitOfWorkParser unitOfWork)
        {
            if (!unitOfWork.ParserWords.Any())
            {
                return;
            }

            foreach (ParserWord word in _parserWords)
            {
                if(unitOfWork.ParserWords.Get(w => w.Name == word.Name) != null)
                {
                    unitOfWork.ParserWords.Delete(word);
                }
            }

            unitOfWork.Save();
        }

        public static void DeinitializeSubtitleRows(IUnitOfWorkParser unitOfWork)
        {
            if (!unitOfWork.SubtitleRows.Any())
            {
                return;
            }

            foreach (SubtitleRow row in _subtitleRows)
            {
                if (unitOfWork.SubtitleRows.Get(n => n.Value == row.Value) != null)
                {
                    unitOfWork.SubtitleRows.Delete(row);
                }
            }

            unitOfWork.Save();
        }
    }
}
