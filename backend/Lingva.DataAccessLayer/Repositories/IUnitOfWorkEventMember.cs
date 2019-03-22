using System;
using System.Collections.Generic;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkEventMember : IUnitOfWork
    {
        IEventMemberRepository EventMembers { get; }
    }
}

