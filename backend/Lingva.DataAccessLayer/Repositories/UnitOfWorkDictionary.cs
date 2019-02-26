using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkDictionary : IUnitOfWorkDictionary
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepository<DictionaryRecord> _dictionaryRecords;
        private readonly IRepository<Word> _words;

        public UnitOfWorkDictionary(DictionaryContext context, IRepository<Word> words, IRepository<DictionaryRecord> dictionaryRecords)
        {
            _context = context;
            _dictionaryRecords = dictionaryRecords;
            _words = words;
        }

        public IRepository<DictionaryRecord> DictionaryRecords { get => _dictionaryRecords; }
        public IRepository<Word> Words { get => _words; }

        //private readonly Func<Type, IRepository<object>> _repositoryDefault;

        //public UnitOfWorkDictionary(DictionaryContext context, Func<Type, IRepository<object>> repositoryDefault)
        //{
        //    _context = context;
        //    _repositoryDefault = repositoryDefault;
        //}

        //public IRepository<DictionaryRecord> DictionaryRecords { get => (IRepository<DictionaryRecord>)GetRepository<DictionaryRecord>(); }
        //public IRepository<Word> Words { get => (IRepository<Word>)GetRepository<Word>(); }

        //public IRepository<object> GetRepository<T>()
        //    where T : class
        //{
        //    return _repositoryDefault(typeof(T));
        //}

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
