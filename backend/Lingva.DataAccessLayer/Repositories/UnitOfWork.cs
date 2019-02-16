using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private static DBContext _context;

        private Repository<DictionaryRecord> _dictionaryRecords;
        private Repository<User> _users;
        private Repository<Phrase> _phrases;
        private Repository<Language> _languages;

        private bool disposed = false;

        public UnitOfWork(DBContext context)
        {
            _context = context;
        }

        public IRepository<DictionaryRecord> Dictionary
        {
            get
            {
                if (_dictionaryRecords == null)
                {
                    _dictionaryRecords = new Repository<DictionaryRecord>(_context);
                }

                return _dictionaryRecords;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new Repository<User>(_context);
                }

                return _users;
            }
        }

        public IRepository<Phrase> Phrases
        {
            get
            {
                if (_phrases == null)
                {
                    _phrases = new Repository<Phrase>(_context);
                }

                return _phrases;
            }
        }

        public IRepository<Language> Languages
        {
            get
            {
                if (_languages == null)
                {
                    _languages = new Repository<Language>(_context);
                }

                return _languages;
            }
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
