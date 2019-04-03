using Lingva.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class UnitOfWorkUserGroup : IUnitOfWorkUserGroup
    {
        private static DictionaryContext _context;

        private bool disposed = false;

        private readonly IRepositoryUser _users;

        private readonly IRepositoryGroup _group;

        private readonly IRepositoryUserGroup _userGroup;

        public IRepositoryGroup Group { get => _group; }
        public IRepositoryUser User { get => _users; }
        public IRepositoryUserGroup userGroup { get => _userGroup; }

        public UnitOfWorkUserGroup(
            DictionaryContext context,
            IRepositoryUserGroup userGroup,
            IRepositoryGroup group,
            IRepositoryUser user)
        {
            _context = context;
            _users = user;
            _group = group;
            _userGroup = userGroup;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

