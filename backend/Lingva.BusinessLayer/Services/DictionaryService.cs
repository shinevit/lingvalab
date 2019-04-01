using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly IUnitOfWorkDictionary _unitOfWork;

        public DictionaryService(IUnitOfWorkDictionary unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DictionaryRecord> GetDictionary()
        {
            return _unitOfWork.DictionaryRecords.GetList();
        }

        public DictionaryRecord GetDictionaryRecord(int id)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.DictionaryRecords.Get(id);
            return dictionaryRecord;
        }

        public void AddDictionaryRecord(DictionaryRecord dictionaryRecord)
        {
            AddWord(dictionaryRecord.WordName);

            if (!ExistDictionaryRecord(dictionaryRecord))
            {
                _unitOfWork.DictionaryRecords.Create(dictionaryRecord);
                _unitOfWork.Save();
            }
        }

        public void UpdateDictionaryRecord(int id, DictionaryRecord dictionaryRecordUpdate)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.DictionaryRecords.Get(id);
            dictionaryRecord.Translation = dictionaryRecordUpdate.Translation;
            _unitOfWork.DictionaryRecords.Update(dictionaryRecord);
            _unitOfWork.Save();
        }

        public void DeleteDictionaryRecord(int id)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.DictionaryRecords.Get(id);

            if (dictionaryRecord == null)
            {
                return;
            }

            _unitOfWork.DictionaryRecords.Delete(dictionaryRecord);
            _unitOfWork.Save();
        }

        private void AddWord(string word)
        {
            if (ExistWord(word))
            {
                return;
            }

            Word newWord = new Word
            {
                Name = word,
                LanguageName = "en",
            };
            _unitOfWork.Words.Create(newWord);
            _unitOfWork.Save();
        }

        private bool ExistWord(string word)
        {
            return _unitOfWork.Words.Get(c => c.Name == word) != null;
        }

        private bool ExistDictionaryRecord(DictionaryRecord dictionaryRecord)
        {
            return _unitOfWork.DictionaryRecords.Get(c => c.UserId == dictionaryRecord.UserId
                                        && c.WordName == dictionaryRecord.WordName
                                        && c.Translation == dictionaryRecord.Translation
                                        && c.LanguageName == dictionaryRecord.LanguageName) != null;
        }
    }
}

