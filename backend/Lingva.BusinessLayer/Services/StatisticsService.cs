using System;
using System.Collections.Generic;
using System.Text;
using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Exceptions;
using System.Linq;
using Lingva.DataAccessLayer;
using Lingva.DataAccessLayer.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace Lingva.BusinessLayer.Services
{
    public class StatisticsService : IStatisticsService
    {
        private const string WRONG_GROUPID = "Group not found";
        private const string WRON_USERID = "User not found";
        private IUnitOfWorkStatistics _unitOfWork;

        public StatisticsService(IUnitOfWorkStatistics unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetUserGroups(int userId, int groupsQuantity)
        {
            var userGroups = _unitOfWork.User.GetList(groupsQuantity, c => c.UserGroups
                                                .Where(u => u.Id == userId) != null);

            if (userGroups == null)
            {
                throw new LingvaException(WRON_USERID);
            }

            return userGroups;
        }

        public IEnumerable<Group> GetGroupParticipants(int groupId, int groupsQuantity)
        {
            var groupUsers = _unitOfWork.Group.GetList(groupsQuantity, u => u.Participants.Where(g => g.Id == groupId) != null);

            if (groupUsers == null)
            {
                throw new LingvaException(WRONG_GROUPID);
            }

            return groupUsers;
        }

    }
}
