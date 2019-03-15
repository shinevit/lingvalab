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
    public class RepositorySubtitleRow : IRepositorySubtitleRow
    {
        private DictionaryContext _context;

        private const string ERR_ARG_NULL_EXP = "Tried to insert null SubtitleRow entity!";

        public RepositorySubtitleRow(DictionaryContext context)
        {
            _context = context;
        }

        public IQueryable<SubtitleRow> GetList()
        {
            return _context.SubtitleRows.AsNoTracking();
        }

        public IQueryable<SubtitleRow> GetList(int quantity, Expression<Func<SubtitleRow, bool>> predicator)
        {
            return _context.SubtitleRows.Where(predicator).Take(quantity).AsNoTracking();
        }

        public SubtitleRow Get(object id)
        {
            return _context.SubtitleRows.Find((int)id);
        }

        public SubtitleRow Get(Expression<Func<SubtitleRow, bool>> predicator)
        {
            return _context.SubtitleRows.Where(predicator).FirstOrDefault();
        }

        public void Create(SubtitleRow subtitle)
        {
            if (subtitle == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.SubtitleRows.Add(subtitle);
        }

        public void Update(SubtitleRow subtitle)
        {
            _context.SubtitleRows.Attach(subtitle);
            _context.Entry(subtitle).State = EntityState.Modified;
        }

        public void Delete(SubtitleRow subtitle)
        {
            if (_context.Entry(subtitle).State == EntityState.Detached)
            {
                _context.SubtitleRows.Attach(subtitle);
            }

            _context.SubtitleRows.Remove(subtitle);
        }
    }
}

