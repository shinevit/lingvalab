using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
