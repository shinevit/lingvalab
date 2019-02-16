using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<DictionaryRecord> Dictionary { get; }
        IRepository<User> Users { get; }
        IRepository<Phrase> Phrases { get; }
        IRepository<Language> Languages { get; }

        void Save();
    }
}
