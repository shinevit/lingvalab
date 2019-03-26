using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkGroupsCollection : IUnitOfWork
    {
        IRepositoryGroup Groups { get; }
    }
}
