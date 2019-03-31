using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Repositories
{
    public class SubtitleRowRepository
    {
        private readonly DictionaryContext _context;

        public SubtitleRowRepository(DictionaryContext context)
        {
            _context = context;
        }

        public void Create(SubtitleRow entity)
        {
            _context.SubtitleRows.Add(entity);
            _context.SaveChanges();
        }

        public void CreateRange(IEnumerable<SubtitleRow> entities)
        {
            _context.SubtitleRows.AddRange(entities);
            _context.SaveChanges();
        }

        public void Delete(SubtitleRow entity)
        {
            _context.SubtitleRows.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<SubtitleRow> Get(Expression<Func<SubtitleRow, bool>> predicate)
        {
            return _context.SubtitleRows.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<SubtitleRow> GetList()
        {
            return _context.SubtitleRows.AsNoTracking();
        }

        public void Update(SubtitleRow entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}