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
    }
}
