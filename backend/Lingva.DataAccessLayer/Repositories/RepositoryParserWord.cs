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
    public class RepositoryParserWord: Repository<ParserWord>, IRepositoryParserWord  
    {
        private const string ERR_ARG_NULL_EXP = "Tried to insert null ParserWord entity!";
        
        public RepositoryParserWord(DictionaryContext context)
            :base(context)
        {
        }

        public IQueryable<ParserWord> GetList()
        {
            return _context.ParserWords.AsNoTracking();
        }

        public IQueryable<ParserWord> GetList(int quantity, Expression<Func<ParserWord, bool>> predicator)
        {
            return _context.ParserWords.Where(predicator).Take(quantity).AsNoTracking();
        }

        public ParserWord Get(object name)
        {
            var result = _context.ParserWords.Where(w => w.Name == name.ToString()).Select(w => w).FirstOrDefault();

            return result;
        }

        public ParserWord Get(Expression<Func<ParserWord, bool>> predicator)
        {
            return _context.ParserWords.Where(predicator).FirstOrDefault();
        }

        public void Create(ParserWord entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.ParserWords.Add(entity);
        }

        public void Update(ParserWord entity)
        {
            _context.ParserWords.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(ParserWord entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.ParserWords.Attach(entity);
            }
            _context.ParserWords.Remove(entity);
        }

        public bool Any()
        {
            return _context.ParserWords.Any();
        }
    }
}

