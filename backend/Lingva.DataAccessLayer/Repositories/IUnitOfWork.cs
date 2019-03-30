using System;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}