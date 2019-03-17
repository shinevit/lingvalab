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
    public class RepositoryDictionaryRecord : Repository<DictionaryRecord>, IRepositoryDictionaryRecord
    {
        private const string ERR_ARG_NULL_EXP = "Tried to insert null DictionaryRecord entity!";

        public RepositoryDictionaryRecord(DictionaryContext context)
            :base(context)
        {
        }

        public IQueryable<DictionaryRecord> GetList()
        {
            return _context.Dictionary.AsNoTracking();
        }

        public IQueryable<DictionaryRecord> GetList(int quantity, Expression<Func<DictionaryRecord, bool>> predicator)
        {
            return _context.Dictionary.Where(predicator).Take(quantity).AsNoTracking();
        }

        public DictionaryRecord Get(object id)
        {
            return _context.Dictionary.Find((int)id);
        }

        public DictionaryRecord Get(Expression<Func<DictionaryRecord, bool>> predicator)
        {
            return _context.Dictionary.Where(predicator).FirstOrDefault();
        }

        public void Create(DictionaryRecord entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.Dictionary.Add(entity);
        }

        public void Update(DictionaryRecord entity)
        {
            _context.Dictionary.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(DictionaryRecord entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Dictionary.Attach(entity);
            }
            _context.Dictionary.Remove(entity);
        }
    }
}
