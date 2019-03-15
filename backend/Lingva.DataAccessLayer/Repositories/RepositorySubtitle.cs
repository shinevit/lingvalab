using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositorySubtitle : IRepositorySubtitle
    {
        private DictionaryContext _context;

        private const string ERR_ARG_NULL_EXP = "Tried to insert null Subtitle entity!";

        public RepositorySubtitle(DictionaryContext context)
        {
            _context = context;
        }

        public IQueryable<Subtitle> GetList()
        {
            return _context.Subtitles.AsNoTracking();
        }

        public IQueryable<Subtitle> GetList(int quantity, Expression<Func<Subtitle, bool>> predicator)
        {
            return _context.Subtitles.Where(predicator).Take(quantity).AsNoTracking();
        }

        public Subtitle Get(object id)
        {
            return _context.Subtitles.Find((int)id);
        }

        public Subtitle Get(Expression<Func<Subtitle, bool>> predicator)
        {
            return _context.Subtitles.Where(predicator).FirstOrDefault();
        }

        public void Create(Subtitle subtitle)
        {
            if (subtitle == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.Subtitles.Add(subtitle);
        }

        public void Update(Subtitle subtitle)
        {
            _context.Subtitles.Attach(subtitle);
            _context.Entry(subtitle).State = EntityState.Modified;
        }

        public void Delete(Subtitle subtitle)
        {
            if (_context.Entry(subtitle).State == EntityState.Detached)
            {
                _context.Subtitles.Attach(subtitle);
            }

            _context.Subtitles.Remove(subtitle);
        }
    }
}
