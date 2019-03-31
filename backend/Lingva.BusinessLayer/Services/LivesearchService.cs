using System.Collections;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Repositories;

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