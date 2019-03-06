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
    public class SubtitlesRowRepository
    {
        private readonly DictionaryContext _context;

        public SubtitlesRowRepository(DictionaryContext context)
        {
            _context = context;
        }
        public void Create(SubtitlesRow entity)
        {
            _context.SubtitlesRows.Add(entity);
            _context.SaveChanges();
        }

        public void CreateRange(IEnumerable<SubtitlesRow> entities)
        {
            _context.SubtitlesRows.AddRange(entities);
            _context.SaveChanges();
        }
        public void Delete(SubtitlesRow entity)
        {
            _context.SubtitlesRows.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<SubtitlesRow> Get(Expression<Func<SubtitlesRow, bool>> predicate)
        {
            return _context.SubtitlesRows.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<SubtitlesRow> GetList()
        {
            return _context.SubtitlesRows.AsNoTracking();
        }

        public void Update(SubtitlesRow entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
