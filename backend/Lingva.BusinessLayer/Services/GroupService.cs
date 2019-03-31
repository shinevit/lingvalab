using System;
using System.Collections.Generic;
using System.Linq;
using Lingva.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Exceptions;

namespace Lingva.BusinessLayer.Services
{
    public class GroupService : IGroupService
    {
        private IRepositoryUserGroup _userGroup;
        
        public GroupService(IRepositoryUserGroup userGroup)
        {
            _userGroup = userGroup;
        }

        public void JoinGroup(int userID, int groupID)
        {
            try
            {
                _userGroup.Create(GetUSerGroupEntity(userID, groupID));
            }
            catch (Exception)
            {
                throw new LingvaException("Failed to join User to group");
            }
        }

        public void LeaveGroup(int userID, int groupID)
        {
            try
            {
                _userGroup.Delete(GetUSerGroupEntity(userID, groupID));
            }
            catch (Exception)
            {
                throw new LingvaException("Failed to leave User to group");
            }
        }
        
        private static UserGroup GetUSerGroupEntity(int userID, int groupID)
        {
            UserGroup userGroup = new UserGroup();
            userGroup.UserId = userID;
            userGroup.GroupId = groupID;

            return userGroup;
        }
    }
}
