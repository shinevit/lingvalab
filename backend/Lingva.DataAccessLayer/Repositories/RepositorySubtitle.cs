using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositorySubtitle : IRepositorySubtitle
    {
        public RepositorySubtitle(DictionaryContext context)
        {

        }

        public void Create(Subtitle entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Subtitle entity)
        {
            throw new NotImplementedException();
        }

        public Subtitle Get(object id)
        {
            throw new NotImplementedException();
        }

        public Subtitle Get(Expression<Func<Subtitle, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Subtitle> GetList()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Subtitle> GetList(int quantity, Expression<Func<Subtitle, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public void Update(Subtitle entity)
        {
            throw new NotImplementedException();
        }
    }
}
