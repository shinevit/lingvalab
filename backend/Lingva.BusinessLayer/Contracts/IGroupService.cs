using System.Collections.Generic;
using Lingva.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lingva.DataAccessLayer.Entities
{
    public interface IGroupService
    {
        void JoinGroup(int userID, int groupID);
        void LeaveGroup(int userID, int groupID);

        IEnumerable<Group> GetGroupsList();

        Group GetGroup(int id);

        Group GetGroupByTitle(string title);

        void AddGroup(Group group, int userId);

        void UpdateGroup(int id, Group group);

        void DeleteGroup(int id);
    }
}
