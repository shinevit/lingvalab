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
    public class RepositoryWord : IRepository<Word>
    {
        private readonly DictionaryContext _context;

        private DbSet<Word> _entities;

        public RepositoryWord(DictionaryContext context)
        {
            _context = context;
            _entities = context.Set<Word>();
        }

        public IQueryable<Word> GetList()
        {
            return _entities.AsNoTracking();
        }

        public IQueryable<Word> GetList(int quantity, Expression<Func<Word, bool>> predicator)
        {
            return _entities.Where(predicator).Take(quantity).AsNoTracking();
        }

        public Word Get(object name)
        {
            return _entities.Find(name.ToString());
        }

        public Word Get(Expression<Func<Word, bool>> predicator)
        {
            return _entities.Where(predicator).FirstOrDefault();
        }

        public void Create(Word entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Tried to insert null entity!");
            }

            _entities.Add(entity);
        }

        public void Update(Word entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Word entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            _entities.Remove(entity);
        }
    }
}
