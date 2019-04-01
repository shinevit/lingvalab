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
    public class RepositoryGroup : Repository<Group>, IRepositoryGroup
    {
        private DictionaryContext _context;

        private const string ERR_ARG_NULL_EXP = "Tried to insert null Group entity!";
        public RepositoryGroup(DictionaryContext context)
            : base(context)
        {
        }

        public IQueryable<Group> GetList()
        {
            return _context.Groups.AsNoTracking();
        }

        public IQueryable<Group> GetList(int quantity, Expression<Func<Group, bool>> predicator)
        {
            return _context.Groups.Where(predicator).Take(quantity).AsNoTracking();
        }

        public Group Get(object id)
        {
            return _context.Groups.Find((int)id);
        }

        public Group Get(Expression<Func<Group, bool>> predicator)
        {
            return _context.Groups.Where(predicator).FirstOrDefault();
        }

        public void Create(Group entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.Groups.Add(entity);
        }

        public void Update(Group entity)
        {
            _context.Groups.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Group entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Groups.Attach(entity);
            }
            _context.Groups.Remove(entity);
        }
    }
}

