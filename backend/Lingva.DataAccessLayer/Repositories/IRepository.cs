using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetList();

        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
