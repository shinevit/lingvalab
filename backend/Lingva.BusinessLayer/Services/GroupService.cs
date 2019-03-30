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
    }
}
