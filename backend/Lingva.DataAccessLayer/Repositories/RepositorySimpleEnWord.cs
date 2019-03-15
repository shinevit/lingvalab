using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositorySimpleEnWord : IRepositorySimpleEnWord
    {
        public void Create(SimpleEnWord entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(SimpleEnWord entity)
        {
            throw new NotImplementedException();
        }

        public SimpleEnWord Get(object id)
        {
            throw new NotImplementedException();
        }

        public SimpleEnWord Get(Expression<Func<SimpleEnWord, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SimpleEnWord> GetList()
        {
            throw new NotImplementedException();
        }

        public IQueryable<SimpleEnWord> GetList(int quantity, Expression<Func<SimpleEnWord, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public void Update(SimpleEnWord entity)
        {
            throw new NotImplementedException();
        }
    }
}
