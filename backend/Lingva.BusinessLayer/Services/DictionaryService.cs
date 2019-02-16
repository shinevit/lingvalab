using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Dto;
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

        public void AddDictionaryRecord(CreateDictionaryRecordDTO createDictionaryRecordDTO)
        {
            DictionaryRecord dictionaryRecord = CreateDictionaryRecord(createDictionaryRecordDTO);
            _unitOfWork.Dictionary.Create(dictionaryRecord);
            try
            {
                _unitOfWork.Save();
            }
            catch
            {
                return;
            }
        }

        public void UpdateDictionaryRecord(int id, CreateDictionaryRecordDTO createDictionaryRecordDTO)
        {
            DictionaryRecord dictionaryRecord = GetDictionaryRecord(id);
            //_unitOfWork.Entry(dictionaryRecord).State = EntityState.Modified;//??
            _unitOfWork.Dictionary.Update(dictionaryRecord);

            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return;
            }
        }
       
        public void DeleteDictionaryRecord(int id)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.Dictionary.Get(id);

            if (dictionaryRecord == null)
            {
                return;
            }

            _unitOfWork.Dictionary.Delete(dictionaryRecord);

            try
            {
                _unitOfWork.Save();
            }
            catch
            {
                return;
            }
        }

        private DictionaryRecord CreateDictionaryRecord(CreateDictionaryRecordDTO createDictionaryRecordDTO)
        {
            DictionaryRecord dictionaryRecord = new DictionaryRecord();

            FillDictionaryRecord(dictionaryRecord, createDictionaryRecordDTO);

            return dictionaryRecord;
        }

        private DictionaryRecord GetDictionaryRecord(int id, CreateDictionaryRecordDTO createDictionaryRecordDTO)
        {
            DictionaryRecord dictionaryRecord = _unitOfWork.Dictionary.Get(id);
            if (dictionaryRecord == null)
            {
                return null;
            }

            FillDictionaryRecord(dictionaryRecord, createDictionaryRecordDTO);

            return dictionaryRecord;
        }

        private void FillDictionaryRecord(DictionaryRecord dictionaryRecord, CreateDictionaryRecordDTO createDictionaryRecordDTO)
        {
            //User owner = _unitOfWork.Users//??
            //              .Where(c => c.Id == createDictionaryRecordDTO.Owner)
            //              .FirstOrDefault();

            //Phrase phrase = _unitOfWork.Phrases
            //                .Where(c => c.Name == createDictionaryRecordDTO.OriginalPhrase)
            //                .FirstOrDefault();

            //Language language = _unitOfWork.Languages
            //                    .Where(c => c.Name == createDictionaryRecordDTO.TranslationLanguage)
            //                    .FirstOrDefault();

            //dictionaryRecord.Owner = owner;
            //dictionaryRecord.OriginalPhrase = phrase;
            dictionaryRecord.TranslationText = createDictionaryRecordDTO.TranslationText;
            //dictionaryRecord.TranslationLanguage = language;
            dictionaryRecord.Context = createDictionaryRecordDTO.Context;
            dictionaryRecord.Picture = createDictionaryRecordDTO.Picture;
        }
    }
}

