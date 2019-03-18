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
    public class RepositoryRole: Repository<Role>, IRepositoryRole
    {
        private DictionaryContext _context;

        private const string ERR_ARG_NULL_EXP = "Tried to insert null Role entity!";
        public RepositoryRole(DictionaryContext context)
            :base(context)
        {
        }

        public IQueryable<Role> GetList()
        {
            return _context.Roles.AsNoTracking();
        }

        public IQueryable<Role> GetList(int quantity, Expression<Func<Role, bool>> predicator)
        {
            return _context.Roles.Where(predicator).Take(quantity).AsNoTracking();
        }

        public Role Get(object name)
        {
            return _context.Roles.Find(name.ToString());
        }

        public Role Get(Expression<Func<Role, bool>> predicator)
        {
            return _context.Roles.Where(predicator).FirstOrDefault();
        }

        public void Create(Role entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(ERR_ARG_NULL_EXP);
            }

            _context.Roles.Add(entity);
        }

        public void Update(Role entity)
        {
            _context.Roles.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Role entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Roles.Attach(entity);
            }
            _context.Roles.Remove(entity);
        }
    }
}

