using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryFilm : IRepositoryFilm
    {
        public RepositoryFilm(DictionaryContext context)
        {

        }

        public void Create(Film entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Film entity)
        {
            throw new NotImplementedException();
        }

        public Film Get(object id)
        {
            throw new NotImplementedException();
        }

        public Film Get(Expression<Func<Film, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Film> GetList()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Film> GetList(int quantity, Expression<Func<Film, bool>> predicator)
        {
            throw new NotImplementedException();
        }

        public void Update(Film entity)
        {
            throw new NotImplementedException();
        }
    }
}
