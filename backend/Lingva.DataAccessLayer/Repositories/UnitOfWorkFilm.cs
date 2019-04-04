using System;
using Lingva.DataAccessLayer.Context;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkFilm : IUnitOfWorkFilm
    {
        private DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepositoryFilm _films;

        public IRepositoryFilm Films { get => _films; }

        public UnitOfWorkFilm(DictionaryContext context, IRepositoryFilm filmRepository)
        {
            _context = context;
            _films = filmRepository;
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