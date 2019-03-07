using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.WordsSelector
{
    public interface IDataProvider : IDisposable
    {
        List<string> GetAllSimpleWords();
        bool AreExist(string word);
        List<string> GetUserDictionary(int userId);
    }
}
