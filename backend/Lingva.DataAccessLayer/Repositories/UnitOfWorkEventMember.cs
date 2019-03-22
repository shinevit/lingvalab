using System;
using System.Collections.Generic;
using System.Text;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkEventMember:IUnitOfWorkEventMember
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IEventMemberRepository _eventMember;

        public IEventMemberRepository EventMembers { get => _eventMember; }

        public UnitOfWorkEventMember(DictionaryContext context, IEventMemberRepository eventMemberRepository)
        {
            _context = context;
            _eventMember = eventMemberRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
