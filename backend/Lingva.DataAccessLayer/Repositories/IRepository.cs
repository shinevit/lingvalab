using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepository<T> : IRepositoryDefault
        where T : class
    {
        IQueryable<T> GetList();
        IQueryable<T> GetList(int quantity, Expression<Func<T, bool>> predicator);

        T Get(object id);
        T Get(Expression<Func<T, bool>> predicator);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
