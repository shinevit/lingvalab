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
    class RepositoryUser : Repository<User>, IUserRepository
    {
        private DbSet<User> _entities;

        private readonly DictionaryContext _context;

        public RepositoryUser(DictionaryContext context)
        {
            _context = context;
            _entities = context.Set<User>();
        }

    }
}
