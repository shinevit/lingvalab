using System;
using System.Collections.Generic;
using System.Text;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Repositories;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkGroup : IUnitOfWorkGroup
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IGroupRepository _group;

        public IGroupRepository Groups { get => _group; }

        public UnitOfWorkGroup(DictionaryContext context, IGroupRepository groupRepository)
        {
            _context = context;
            _group = groupRepository;
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
