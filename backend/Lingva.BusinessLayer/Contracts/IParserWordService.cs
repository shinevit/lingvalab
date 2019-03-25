using Lingva.BusinessLayer.DTO;
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

        IEnumerable<ParserWordDTO> AddParserWordsFromRow(SubtitleRow row);

        void AddParserWord(ParserWord word);

        void InsertOrUpdateParserWord(ParserWord word);

        ParserWord DeleteParserWord(string name);

        bool ExistsParserWord(string wordName);
    }
}
