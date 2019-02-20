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
    public class FilmRepository
    {
        private readonly DictionaryContext _context;

        public FilmRepository(DictionaryContext context)
        {
            _context = context;
        }
        public void Create(Film entity)
        {
            _context.Films.Add(entity);
            _context.SaveChanges();
        }

        public void CreateRange(IEnumerable<Film> entities)
        {
            _context.Films.AddRange(entities);
            _context.SaveChanges();
        }
        public void Delete(Film entity)
        {
            _context.Films.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Film> Get(Expression<Func<Film, bool>> predicate)
        {
            return _context.Films.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<Film> GetList()
        {
            return _context.Films.AsNoTracking();
        }

        public void Update(Film entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
