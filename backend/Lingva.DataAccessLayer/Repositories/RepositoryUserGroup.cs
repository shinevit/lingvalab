using Lingva.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryUserGroup : Repository<UserGroup>, IRepositoryUserGroup
    {
        public RepositoryUserGroup(DictionaryContext context) : base(context)
        {
        }
    }
}
