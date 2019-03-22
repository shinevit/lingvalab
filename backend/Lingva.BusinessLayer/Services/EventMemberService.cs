using System;
using System.Collections.Generic;
using System.Linq;
using Lingva.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Context;


namespace UsersManagementBackEnd.Services
{
    public class EventMemberService : IEventMemberService
    {
        private IUnitOfWorkEventMember _unitOfWork;
        private IUnitOfWorkUser _unitOfWorkUser;

        public EventMemberService(IUnitOfWorkEventMember unitOfWorkEventMember, IUnitOfWorkUser unitOfWorkUser)
        {
            _unitOfWork = unitOfWorkEventMember;
            _unitOfWorkUser = unitOfWorkUser;
        }

        public EventMember JoinGroup(int userID, int groupID)
        {
            var newMember = new EventMember { UserID = userID, EventID = groupID };
            _unitOfWork.EventMembers.Create(newMember);
            
            return newMember;
        }


        public void LeaveGroup(int userID,  int groupID)
        {            
            _unitOfWork.EventMembers.Delete(new EventMember { UserID = userID, EventID = groupID });
        }       

        public IEnumerable<EventMember> GetAll()
        {            
            return _unitOfWork.EventMembers.GetList();
        }
    }
}
