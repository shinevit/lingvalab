using System;
using System.Collections.Generic;
using System.Linq;
using Lingva.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Context;

namespace Lingva.BusinessLayer.Services
{
    public class GroupService : IGroupService
    {
        private IUnitOfWorkGroup _unitOfWork;
        private IUnitOfWorkUser _unitOfWorkUser;

        public GroupService(IUnitOfWorkGroup unitOfWorkGroup, IUnitOfWorkUser unitOfWorkUser)
        {
            _unitOfWork = unitOfWorkGroup;
            _unitOfWorkUser = unitOfWorkUser;

        }

        public Group JoinGroup(int userID, int groupID)
        {
            //e is no Subtitle record with Path = {path} in the Subtitles table.");  var newGroup = new Group { UserId = userID, EventId = groupID};
            // _unitOfWork.Groups.Create(newGroup);

            return new Group();
        }

        public void LeaveGroup(int userID, int groupID)
        {
            //_unitOfWork.Groups.Delete(new Group { UserId = userID, EventId = groupID});
        }

        public IEnumerable<Group> GetAll()
        {
            return _unitOfWork.Groups.GetList();
        }

        public IEnumerable<Group> GetGroupsList()
        {
            return _unitOfWork.Groups.GetList();
        }

        public Group GetGroup(int id)
        {
            Group group = _unitOfWork.Groups.Get(id);
            return group;
        }

        public Group GetGroupByTitle(string title)
        {
            Group group = _unitOfWork.Groups.Get(g => g.Title.Contains(title));
            return group;
        }

        public void AddGroup(Group group)
        {
            _unitOfWork.Groups.Create(group);
            _unitOfWork.Save();
        }

        public void UpdateGroup(int id, Group group)
        {
            Group myEvent = _unitOfWork.Groups.Get(id);
            _unitOfWork.Groups.Update(group);
            _unitOfWork.Save();
        }

        public void DeleteGroup(int id)
        {
            Group group = _unitOfWork.Groups.Get(id);

            if (group == null)
            {
                return;
            }

            _unitOfWork.Groups.Delete(group);
            _unitOfWork.Save();
        }
    }
}
