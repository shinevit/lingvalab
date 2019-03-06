using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryDictionaryRecord : IRepository<DictionaryRecord>
    {
        private readonly DictionaryContext _context;

        private DbSet<DictionaryRecord> _entities;

        public RepositoryDictionaryRecord(DictionaryContext context)
        {
            _context = context;
            _entities = context.Set<DictionaryRecord>();
        }

        public IQueryable<DictionaryRecord> GetList()
        {
            return _entities.AsNoTracking();
        }

        public IQueryable<DictionaryRecord> GetList(int quantity, Expression<Func<DictionaryRecord, bool>> predicator)
        {
            return _entities.Where(predicator).Take(quantity).AsNoTracking();
        }

        public DictionaryRecord Get(object id)
        {
            return _entities.Find((int)id);
        }

        public DictionaryRecord Get(Expression<Func<DictionaryRecord, bool>> predicator)
        {
            return _entities.Where(predicator).FirstOrDefault();
        }

        public void Create(DictionaryRecord entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Tried to insert null entity!");
            }

            _entities.Add(entity);
        }

        public void Update(DictionaryRecord entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(DictionaryRecord entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            _entities.Remove(entity);
        }
    }
}
