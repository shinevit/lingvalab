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
    public class RepositoryEvent : Repository<Event>, IRepositoryEvent
    {
        private const string ERR_ARG_NULL_EXP = "Tried to insert null Event entity!";
        public RepositoryEvent(DictionaryContext context)
            : base(context)
        {
        }

        public IQueryable<Event> GetList()
        {
            return _context.Events.AsNoTracking();
        }

        public IQueryable<Event> GetList(int quantity, Expression<Func<Event, bool>> predicator)
        {
            return _context.Events.Where(predicator).Take(quantity).AsNoTracking();
        }

        public Event Get(object id)
        {
            return _context.Events.Find((int)id);
        }

        public Event Get(Expression<Func<Event, bool>> predicator)
        {
            return _context.Events.Where(predicator).FirstOrDefault();
        }

        public void Create(Event entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.Events.Add(entity);
        }

        public void Update(Event entity)
        {
            _context.Events.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Event entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Events.Attach(entity);
            }
            _context.Events.Remove(entity);
        }
    }
}

