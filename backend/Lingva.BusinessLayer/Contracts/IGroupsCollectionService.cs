using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.BusinessLayer.Contracts
{
    public interface IGroupsCollectionService
    {
        IEnumerable<Group> GetGroupsList();

        Group GetGroup(int id);

        void AddGroup(Group group);

        void UpdateGroup(int id, Group group);

        void DeleteGroup(int id);
    }
}
