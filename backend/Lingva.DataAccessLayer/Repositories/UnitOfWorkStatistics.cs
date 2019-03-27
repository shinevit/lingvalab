using Lingva.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    class UnitOfWorkStatistics : IUnitOfWorkStatistics
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepositoryUser _users;
        private readonly IRepositoryEvent _event;
        private readonly IRepositoryGroup _group;

        public IRepositoryGroup Group { get => _group; }
        public IRepositoryUser User { get => _users; }
        public IRepositoryEvent Event { get => _event; }

        public UnitOfWorkStatistics(
            DictionaryContext context,
            IRepositoryEvent lingvaEvent,
            IRepositoryGroup group,
            IRepositoryUser user)
        {
            _context = context;
            _users = user;
            _group = group;
            _event = lingvaEvent;
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

