using System.Collections.Generic;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IWordService
    {
        IEnumerable<ParserWord> GetAll();
        ParserWord GetParserWordByName(string name);
        bool AddWordsFromRow(SubtitleRow row);
        bool AddWordFromPhrase(string phrase, string language = "en", int? rowId = null);
        bool ExistsWord(string wordName);
    }
}