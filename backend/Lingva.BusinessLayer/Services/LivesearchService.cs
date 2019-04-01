using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.BusinessLayer.Services
{
    public class LivesearchService : ILivesearchService
    {
        private readonly IUnitOfWorkDictionary _unitOfWork;

        public LivesearchService(IUnitOfWorkDictionary unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable Find(string substring, int quantity)
        {
            var result = _unitOfWork.DictionaryRecords.GetList(quantity, w => w.WordName.Contains(substring));
            return result;
        }
    }
}
