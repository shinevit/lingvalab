using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IParserWordService
    {
        ParserWord GetParserWordWhere(Expression<Func<ParserWord, bool>> predicate);

        ParserWord GetParserWord(string name);

        IQueryable<ParserWord> GetAllParserWords();

        bool AddWordsFromRow(SubtitleRow row);

        bool AddWordFromPhrase(string phrase, string language = "en", int? rowId = null);

        void AddParserWord(ParserWord word);

        void UpdateParserWord(ParserWord parserWord);

        void DeleteParserWord(string name);

        bool ExistsParserWord(string wordName);
    }
}
