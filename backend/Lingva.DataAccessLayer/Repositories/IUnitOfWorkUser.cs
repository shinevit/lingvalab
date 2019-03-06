using System;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkUser: IUnitOfWork
    {
        IRepository<User> Users { get; }
    }
}
