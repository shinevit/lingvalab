using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositorySubtitleRow : IRepositorySubtitleRow
    {
        public RepositorySubtitleRow(DictionaryContext context) 
        {

        }

        public void Create(SubtitleRow entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(SubtitleRow entity)
        {
            throw new NotImplementedException();
        }

        public SubtitleRow Get(object id)
        {
            throw new NotImplementedException();
        }

        public SubtitleRow Get(Expression<Func<SubtitleRow, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SubtitleRow> GetList()
        {
            throw new NotImplementedException();
        }

        public IQueryable<SubtitleRow> GetList(int quantity, Expression<Func<SubtitleRow, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public void Update(SubtitleRow entity)
        {
            throw new NotImplementedException();
        }
    }
}
