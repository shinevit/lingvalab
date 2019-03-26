using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkMovieCollection : IUnitOfWorkMovieCollection
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepositoryMovie _movies;

        public UnitOfWorkMovieCollection(DictionaryContext context, IRepositoryMovie movies)
        {
            _context = context;
            _movies = movies;
        }

        public IRepositoryMovie Movies { get => _movies; }

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
