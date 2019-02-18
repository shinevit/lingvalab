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
        private readonly IUnitOfWork _unitOfWork;

        public DictionaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DictionaryRecord> GetDictionary()
        {
            return _unitOfWork.Dictionary.GetList();
        }

        public DictionaryRecord GetDictionaryRecord(int id)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.Dictionary.Get(id);
            return dictionaryRecord;
        }

        public void AddDictionaryRecord(DictionaryRecord dictionaryRecord)
        {
            AddWord(dictionaryRecord.OriginalPhraseName);
            if (!ExistDictionaryRecord(dictionaryRecord))
            {
                _unitOfWork.Dictionary.Create(dictionaryRecord);
                _unitOfWork.Save();
            }
        }

        public void UpdateDictionaryRecord(int id, DictionaryRecord dictionaryRecordUpdate)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.Dictionary.Get(id);
            dictionaryRecord.TranslationText = dictionaryRecordUpdate.TranslationText;
            _unitOfWork.Dictionary.Update(dictionaryRecord);
            _unitOfWork.Save();
        }
       
        public void DeleteDictionaryRecord(int id)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.Dictionary.Get(id);

            if (dictionaryRecord == null)
            {
                return;
            }

            _unitOfWork.Dictionary.Delete(dictionaryRecord);
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
            return _unitOfWork.Dictionary.Get(c => c.UserId == dictionaryRecord.UserId
                                        && c.OriginalPhraseName == dictionaryRecord.OriginalPhraseName
                                        && c.TranslationText == dictionaryRecord.TranslationText
                                        && c.TranslationLanguageName == dictionaryRecord.TranslationLanguageName) != null;
        }
    }
}

