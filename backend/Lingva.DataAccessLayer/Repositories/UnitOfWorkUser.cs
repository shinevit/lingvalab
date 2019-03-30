using System;
using Lingva.DataAccessLayer.Context;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkUser : IUnitOfWorkUser
    {
        private static DictionaryContext _context;

        private bool disposed;

        public UnitOfWorkUser(DictionaryContext context, IRepositoryUser userRepository)
        {
            _context = context;
            Users = userRepository;
        }

        public IRepositoryUser Users { get; }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _context.Dispose();
            disposed = true;
        }
    }
}