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
        private IUnitOfWorkEventMember _unitOfWork;
        private IUnitOfWorkUser _unitOfWorkUser;

        public GroupService(IUnitOfWorkEventMember unitOfWorkEventMember, IUnitOfWorkUser unitOfWorkUser)
        {
            _unitOfWork = unitOfWorkEventMember;
            _unitOfWorkUser = unitOfWorkUser;
        }

        public Group JoinGroup(int userID, int groupID)
        {
            var newGroup = new Group { UserId = userID, EventId = groupID};
            _unitOfWork.Groups.Create(newGroup);

            return newGroup;
        }

        public void LeaveGroup(int userID, int groupID)
        {
            _unitOfWork.Groups.Delete(new Group { UserId = userID, EventId = groupID});
        }

        public IEnumerable<Group> GetAll()
        {
            return _unitOfWork.Groups.GetList();
        }

        public Group JoinGroup(int userID, int groupID, string roleName = "Student")
        {
            var newGroup = new Group { UserId = userID, EventId = groupID, RoleName = roleName };
            _unitOfWork.Groups.Create(newGroup);

            return newGroup;
        }

        public void LeaveGroup(int userID, int groupID, string roleName = "Student")
        {
            _unitOfWork.Groups.Delete(new Group { UserId = userID, EventId = groupID, RoleName = roleName });
        }        
    }
}
