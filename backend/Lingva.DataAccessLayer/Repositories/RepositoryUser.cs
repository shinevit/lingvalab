using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    class RepositoryUser : Repository<User>, IRepositoryUser
    {
        public RepositoryUser(DictionaryContext context) : base(context)
        {
        }
    }
}
