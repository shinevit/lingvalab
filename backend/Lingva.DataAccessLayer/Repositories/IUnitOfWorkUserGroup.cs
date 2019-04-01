using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkUserGroup : IUnitOfWork
    {
        IRepositoryUser User { get; }
        IRepositoryGroup Group { get; }
        IRepositoryUserGroup userGroup { get; }
    }
}

