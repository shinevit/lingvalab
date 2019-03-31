using System;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWork 
    {
        void Save();
    }
}