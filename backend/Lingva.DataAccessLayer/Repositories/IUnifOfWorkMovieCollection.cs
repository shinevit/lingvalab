using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkMovieCollection : IUnitOfWork
    {
        IRepositoryMovie Movies { get; }
    }
}

