using System.Collections.Generic;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IDictionaryService
    {
        IEnumerable<DictionaryRecord> GetDictionary();

        DictionaryRecord GetDictionaryRecord(int id);

        void AddDictionaryRecord(DictionaryRecord dictionaryRecord);

        void UpdateDictionaryRecord(int id, DictionaryRecord dictionaryRecord);

        void DeleteDictionaryRecord(int id);
    }
}