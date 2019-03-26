using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkGroupsCollection : IUnitOfWorkGroupsCollection
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepositoryGroup _groups;

        public UnitOfWorkGroupsCollection(DictionaryContext context, IRepositoryGroup groups)
        {
            _context = context;
            _groups = groups;
        }

        public IRepositoryGroup Groups { get => _groups; }

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
