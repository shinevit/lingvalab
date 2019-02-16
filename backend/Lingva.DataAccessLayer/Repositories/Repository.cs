using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Lingva.DataAccessLayer.Context;

namespace Lingva.DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DBContext _context;

        private DbSet<T> _entities;

        public Repository(DBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetList()
        {
            return _entities.AsNoTracking();
        }

        public T Get(int id)
        {
            return _entities.Find(id);
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Tried to insert null entity!");
            }

            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
