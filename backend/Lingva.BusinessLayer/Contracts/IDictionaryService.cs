using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
