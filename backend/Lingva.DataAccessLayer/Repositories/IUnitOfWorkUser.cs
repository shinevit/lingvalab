using System;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkUser : IUnitOfWork
    {
        IRepositoryUser Users { get; }
    }
}
