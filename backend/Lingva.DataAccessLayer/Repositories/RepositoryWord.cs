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
    public class RepositoryWord : Repository<Word>, IRepositoryWord
    {
        
        private const string ERR_ARG_NULL_EXP = "Tried to insert null Word entity!";

        public RepositoryWord(DictionaryContext context)
            : base(context)
        {
        }

        public IQueryable<Word> GetList()
        {
            return _context.Words.AsNoTracking();
        }

        public IQueryable<Word> GetList(int quantity, Expression<Func<Word, bool>> predicator)
        {
            return _context.Words.Where(predicator).Take(quantity).AsNoTracking();
        }

        public Word Get(object name)
        {
            return _context.Words.Find(name.ToString());
        }

        public Word Get(Expression<Func<Word, bool>> predicator)
        {
            return _context.Words.Where(predicator).FirstOrDefault();
        }

        public void Create(Word entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.Words.Add(entity);
        }

        public void Update(Word entity)
        {
            _context.Words.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Word entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Words.Attach(entity);
            }
            _context.Words.Remove(entity);
        }
    }
}
