using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkStatistics:IUnitOfWork
    {
        IRepositoryUser User { get; }
        IRepositoryGroup Group { get; }
        IRepositoryEvent Event { get; }
    }
}

