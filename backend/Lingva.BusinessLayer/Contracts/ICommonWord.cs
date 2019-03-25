using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Interfaces
{
    public interface ICommonWord : IDisposable
    {
        List<string> GetAllSimpleWords();
        bool AreExist(string word);
        List<string> GetUserDictionary(int userId);
    }
}
