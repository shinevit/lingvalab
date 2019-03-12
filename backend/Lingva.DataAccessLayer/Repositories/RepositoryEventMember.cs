using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;


namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryEventMember: IRepositoryEventMembers
    {
        private readonly ApplicationContext _context = new ApplicationContext();
        private DbSet<EventMembers> _eventMembers { get; set; }

        private const string ERR_ARG_NULL_EXP = "Tried to insert null Group entity!";
        public RepositoryEventMember(ApplicationContext context)
        {
            _context = context;
            _eventMembers = context.Set<EventMembers>();
        }

        public RepositoryEventMember()
        {

        }

        public  IQueryable<EventMembers> GetList() { return null; }
        public  IQueryable<EventMembers> GetList(int quantity, Expression<Func<EventMembers, bool>> predicator) { return null; }

        public EventMembers Get(object id) { return null; }
        public EventMembers Get(Expression<Func<EventMembers, bool>> predicator) { return null; }

        public void Create(EventMembers entity)
        {
            _context.EventMembers.Add(entity: entity);
            _context.SaveChanges();
        }

        public void Update(EventMembers entity)
        {
            
        }

        public void Delete(EventMembers entity)
        {
            _context.EventMembers.Remove(entity: entity);
        }
    }
}
