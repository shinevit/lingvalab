﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Exceptions;
using Lingva.DataAccessLayer.Repositories;



namespace Lingva.BusinessLayer.Services
{
    public class StatisticsService : IStatisticsService
    {
        private const string WRONG_GROUPID = "Group not found";
        private const string WRON_USERID = "User not found";
        private IUnitOfWorkUserGroup _unitOfWork;

        public StatisticsService(IUnitOfWorkUserGroup unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetGroupParticipants(int groupId)
        {

            var usersID = _unitOfWork.userGroup.GetList(g => g.GroupId == groupId);

            if (usersID == null)
            {
                throw new StatisticsServiceException(WRONG_GROUPID);
            }

            foreach (var item in usersID)
            {
                yield return _unitOfWork.User.Get(g => g.Id == (item.UserId));
            }
        }

        public IEnumerable<Group> GetUserGroups(int userId)
        {
            var groupsID = _unitOfWork.userGroup.GetList(g => g.UserId == userId);

            if (groupsID == null)
            {
                throw new StatisticsServiceException(WRON_USERID);
            }

            foreach (var item in groupsID)
            {
                yield return _unitOfWork.Group.Get(g => g.Id == (item.GroupId));
            }

        }

    }
}
