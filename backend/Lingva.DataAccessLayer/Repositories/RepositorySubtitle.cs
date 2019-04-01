using System;
using System.Linq;
using System.Linq.Expressions;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositorySubtitle : Repository<Subtitles>, IRepositorySubtitle
    {
        private const string ERR_ARG_NULL_EXP = "Tried to insert null Subtitle entity!";

        public RepositorySubtitle(DictionaryContext context)
            : base(context)
        {
        }

        public IQueryable<Subtitles> GetList()
        {
            return _context.Subtitles.AsNoTracking();
        }

        public IQueryable<Subtitles> GetList(int quantity, Expression<Func<Subtitles, bool>> predicator)
        {
            return _context.Subtitles.Where(predicator).Take(quantity).AsNoTracking();
        }

        public Subtitles Get(object id)
        {
            return _context.Subtitles.Find((int) id);
        }

        public Subtitles Get(Expression<Func<Subtitles, bool>> predicator)
        {
            return _context.Subtitles.Where(predicator).FirstOrDefault();
        }

        public void Create(Subtitles subtitle)
        {
            if (subtitle == null) throw new ArgumentNullException(ERR_ARG_NULL_EXP);

            _context.Subtitles.Add(subtitle);
        }

        public void Update(Subtitles subtitle)
        {
            _context.Subtitles.Attach(subtitle);
            _context.Entry(subtitle).State = EntityState.Modified;
        }

        public void Delete(Subtitles subtitle)
        {
            if (_context.Entry(subtitle).State == EntityState.Detached) _context.Subtitles.Attach(subtitle);

            _context.Subtitles.Remove(subtitle);
        }
    }
}