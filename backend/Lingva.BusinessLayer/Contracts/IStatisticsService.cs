using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IStatisticsService
    {
        IEnumerable<User> GetUserGroups(int userId, int groupsQuantity);
        IEnumerable<Group> GetGroupParticipants(int groupId, int groupsQuantity);
    }
}
