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
        private readonly IUnitOfWork _unitOfWork;
        
        public LivesearchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable Find(string substring, int quantity)
        {
            var result = _unitOfWork.Words.GetList(quantity, w => w.Name.Contains(substring));
            return result;
        }
    }
}
