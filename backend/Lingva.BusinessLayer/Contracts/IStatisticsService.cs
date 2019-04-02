using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IStatisticsService
    {
        IEnumerable<Group> GetUserGroups(int userId);
        IEnumerable<User> GetGroupParticipants(int groupId);
    }
}
