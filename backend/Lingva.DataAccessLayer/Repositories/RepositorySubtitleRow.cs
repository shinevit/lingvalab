using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositorySubtitleRow : Repository<SubtitleRow>, IRepositorySubtitleRow
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private const string ERR_ARG_NULL_EXP = "Tried to insert null SubtitleRow entity!";

        public RepositorySubtitleRow(DictionaryContext context)
            : base(context)
        {
        }

        public override IQueryable<SubtitleRow> GetList()
        {
            return _context.SubtitleRows.AsNoTracking();
        }

        public override IQueryable<SubtitleRow> GetList(int quantity, Expression<Func<SubtitleRow, bool>> predicator)
        {
            return _context.SubtitleRows.Where(predicator).Take(quantity).AsNoTracking();
        }

        public override SubtitleRow Get(object id)
        {
            return _context.SubtitleRows.Find((int)id);
        }

        public override SubtitleRow Get(Expression<Func<SubtitleRow, bool>> predicator)
        {
            return _context.SubtitleRows.Where(predicator).FirstOrDefault();
        }

        public override void Create(SubtitleRow subtitle)
        {
            if (subtitle == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.SubtitleRows.Add(subtitle);
        }

        public override void Update(SubtitleRow subtitle)
        {
            _context.SubtitleRows.Attach(subtitle);
            _context.Entry(subtitle).State = EntityState.Modified;
        }

        public override void Delete(SubtitleRow subtitle)
        {
            if (_context.Entry(subtitle).State == EntityState.Detached)
            {
                _context.SubtitleRows.Attach(subtitle);
            }

            _context.SubtitleRows.Remove(subtitle);
        }

        public void InsertOrUpdate(SubtitleRow subtitle)
        {
            {
                return;
            }

            if (Get(n => n.Value == subtitle.Value) != null)
            {
                _context.Update(subtitle);
                return;
            }

            _context.SubtitleRows.Add(subtitle);
        }

        public bool Any()
        {
            return _context.SubtitleRows.Any();
        }

        public bool Exists(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _logger.Warn("Tried to check the SubtitleRow record with Value is empty or null for existence.",
                    new ArgumentNullException());

                throw new ArgumentNullException("Tried to check the SubtitleRow record with Value is empty or null for existence.");
            }

            return _context.SubtitleRows.Any(r => r.Value == value);
        }
    }
}




