using System;
using System.Linq;
using System.Linq.Expressions;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryFilm : Repository<Film>, IRepositoryFilm
    {
        private const string ERR_ARG_NULL_EXP = "Tried to insert null Film entity!";
        protected DictionaryContext _context;

        public RepositoryFilm(DictionaryContext context)
            : base(context)
        {
        }

        public IQueryable<Film> GetList()
        {
            return _context.Films.AsNoTracking();
        }

        public IQueryable<Film> GetList(int quantity, Expression<Func<Film, bool>> predicator)
        {
            return _context.Films.Where(predicator).Take(quantity).AsNoTracking();
        }

        public override Film Get(object id)
        {
            return _context.Films.Find((int) id);
        }

        public Film Get(Expression<Func<Film, bool>> predicator)
        {
            return _context.Films.Where(predicator).FirstOrDefault();
        }

        public void Create(Film entity)
        {
            if (entity == null) throw new ArgumentNullException(ERR_ARG_NULL_EXP);

            _context.Films.Add(entity);
        }

        public void Update(Film entity)
        {
            _context.Films.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Film entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached) _context.Films.Attach(entity);
            _context.Films.Remove(entity);
        }
    }
}