using Lingva.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public abstract class Repository<T> : IRepository<T> 
        where T : class
    {
        protected DictionaryContext _context;
        protected DbSet<T> _entities;

        public Repository(DictionaryContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual IQueryable<T> GetList()
        {
            return _entities.AsNoTracking();
        }

        public virtual IQueryable<T> GetList(int quantity, Expression<Func<T, bool>> predicator)
        {
            return _entities.Where(predicator).Take(quantity).AsNoTracking();
        }

        public virtual T Get(object id)
        {
            return _entities.Find((int)id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicator)
        {
            return _entities.Where(predicator).FirstOrDefault();
        }

        public virtual void Create(T entity)
        {
            if (_entities == null)
            {
                throw new ArgumentNullException("Tried to work with null entity set!");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("Tried to insert null entity!");
            }

            _entities.Add(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Tried to update null entity!");
            }

            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Tried to delete null entity!");
            }

            if(!_entities.Select(e => e == entity).Any())
            {
                throw new ArgumentNullException("Attempt to delete non-existent entity has occured.");
            }

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            _entities.Remove(entity);
        }
    }
}
