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

        IEnumerable<ParserWord> GetAllParserWords();

        bool AddParserWordsFromSubtitleRow(SubtitleRow row);

        bool AddParserWordsFromPhrase(string phrase, string language = "en", int? rowId = null);

        bool AddParserWord(string word, string language = "en", int? subtitleRowId = null);

        bool AddWord(ParserWord word);

        void UpdateParserWord(ParserWord parserWord);

        void DeleteParserWord(string name);

        bool ExistsParserWord(string wordName);
    }
}
