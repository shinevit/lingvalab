using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryDictionaryEnWord : IRepositoryDictionaryEnWord
    {
        public void Create(DictionaryEnWord entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DictionaryEnWord entity)
        {
            throw new NotImplementedException();
        }

        public DictionaryEnWord Get(object id)
        {
            throw new NotImplementedException();
        }

        public DictionaryEnWord Get(Expression<Func<DictionaryEnWord, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DictionaryEnWord> GetList()
        {
            throw new NotImplementedException();
        }

        public IQueryable<DictionaryEnWord> GetList(int quantity, Expression<Func<DictionaryEnWord, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public void Update(DictionaryEnWord entity)
        {
            throw new NotImplementedException();
        }
    }
}
