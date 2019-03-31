using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Repositories
{
    public class SubtitlesRepository
    {
        private readonly DictionaryContext _context;

        public SubtitlesRepository(DictionaryContext context)
        {
            _context = context;
        }

        public void Create(Subtitles entity)
        {
            _context.Subtitles.Add(entity);
            _context.SaveChanges();
        }

        public void CreateRange(IEnumerable<Subtitles> entities)
        {
            _context.Subtitles.AddRange(entities);
            _context.SaveChanges();
        }

        public void Delete(Subtitles entity)
        {
            _context.Subtitles.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Subtitles> Get(Expression<Func<Subtitles, bool>> predicate)
        {
            return _context.Subtitles.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<Subtitles> GetList()
        {
            return _context.Subtitles.AsNoTracking();
        }

        public void Update(Subtitles entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}