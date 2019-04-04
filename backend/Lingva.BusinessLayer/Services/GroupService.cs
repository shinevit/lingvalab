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

        private IUnitOfWorkUserGroup _unitOfWork;

        public GroupService(IUnitOfWorkUserGroup unitOfWorkUserGroup)
        {
            _unitOfWork = unitOfWorkUserGroup;
        }

        public void JoinGroup(int userID, int groupID)
        {

            if (_unitOfWork.userGroup.Get
                (c => c.GroupId == groupID && c.UserId == userID)!=null)
            {
                throw new LingvaException("User have already participating in this group");
            }            
            try
            {
                _unitOfWork.userGroup.Create(GetUserGroupEntity(userID, groupID));
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new LingvaException("Failed to join User to group");
            }
        }

        public void LeaveGroup(int userID, int groupID)
        {

            if (_unitOfWork.userGroup.Get
               (c => c.GroupId == groupID && c.UserId == userID) == null)
            {
                throw new LingvaException("User haven't been participating in this group");
            }
            try
            {
                _unitOfWork.userGroup.Delete(GetUserGroupEntity(userID, groupID));
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new LingvaException("Failed to remove User from group");
            }
        }

        public IEnumerable<Group> GetGroupsList()
        {
            return _unitOfWork.Group.GetList();
        }

        public Group GetGroup(int id)
        {
            Group group = _unitOfWork.Group.Get(id);
            return group;
        }

        public Group GetGroupByTitle(string title)
        {
            Group group = _unitOfWork.Group.Get(g => g.Title.Contains(title));
            return group;
        }

        public void AddGroup(Group group, int userId)
        {
            _unitOfWork.Group.Create(group);
            _unitOfWork.userGroup.Create(GetUserGroupEntity(userId, group.Id));
            _unitOfWork.Save();
        }

        public void UpdateGroup(int id, Group group)
        {
            Group myEvent = _unitOfWork.Group.Get(id);
            _unitOfWork.Group.Update(group);
            _unitOfWork.Save();
        }

        public void DeleteGroup(int id)
        {
            Group group = _unitOfWork.Group.Get(id);

            if (group == null)
            {
                return;
            }

            _unitOfWork.Group.Delete(group);
            _unitOfWork.Save();
        }

        private static UserGroup GetUserGroupEntity(int userID, int groupID)
        {
            UserGroup userGroup = new UserGroup();
            userGroup.UserId = userID;
            userGroup.GroupId = groupID;

            return userGroup;
        }

    }
}
