using Lingva.DataAccessLayer.Dto;
using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Services
{
    public interface IDictionaryService
    {
        IEnumerable<DictionaryRecord> GetDictionary();

        DictionaryRecord GetDictionaryRecord(int id);

        void AddDictionaryRecord(CreateDictionaryRecordDTO createDictionaryRecordDTO);

        void UpdateDictionaryRecord(int id, CreateDictionaryRecordDTO createDictionaryRecordDTO);

        void DeleteDictionaryRecord(int id);
    }
}
