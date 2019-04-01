using System;
using System.Collections.Generic;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkGroup : IUnitOfWork
    {
        IGroupRepository Groups { get; }
    }
}

