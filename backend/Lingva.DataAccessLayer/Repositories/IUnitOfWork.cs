using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWork 
    {
        void Save();
    }
}
