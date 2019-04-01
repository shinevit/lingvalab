using System;
using Lingva.DataAccessLayer.Context;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkDictionary : IUnitOfWorkDictionary
    {
        private static DictionaryContext _context;

        private bool disposed;

        public UnitOfWorkDictionary(DictionaryContext context, IRepositoryWord words,
            IRepositoryDictionaryRecord dictionaryRecords)
        {
            _context = context;
            DictionaryRecords = dictionaryRecords;
            Words = words;
        }

        public IRepositoryDictionaryRecord DictionaryRecords { get; }
        public IRepositoryWord Words { get; }

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