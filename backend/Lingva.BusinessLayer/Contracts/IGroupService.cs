using System.Collections.Generic;
using Lingva.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.DataAccessLayer.Entities
{
    public interface IGroupService
    {
        Group JoinGroup(int userID, int groupID);
        void LeaveGroup(int userID, int groupID);

        IEnumerable<Group> GetGroupsList();

        Group GetGroup(int id);

        Group GetGroupByTitle(string title);

        void AddGroup(Group group);

        void UpdateGroup(int id, Group group);

        void DeleteGroup(int id);
    }
}
