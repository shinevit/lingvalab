using System;
using System.Collections.Generic;
using System.Linq;
using Lingva.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Lingva.DataAccessLayer.Repositories;
using Lingva.DataAccessLayer.Context;


namespace UsersManagementBackEnd.Services
{
    public class EventMemberService : IMembersService
    {
        public ApplicationContext _context;
        RepositoryEventMember repositoryEventMember;

        public EventMemberService()
        {
            _context = new ApplicationContext();

            repositoryEventMember = new RepositoryEventMember();
        }

        

        public EventMembers JoinGroup(int userID, int groupID)
        {
            var memberID = _context.EventMembers.LastOrDefault().ID;

            var newMember = new EventMembers{UserID = userID,
                                            EventID = groupID,
                                            ID = _context.EventMembers.LastOrDefault().ID};

            repositoryEventMember.Create(newMember);

            return newMember;
        }


        public void LeaveGroup(int userID,  int groupID)
        { 
            var removingMember = _context.EventMembers.Single( em => ((em.UserID == userID) && (em.EventID == groupID)));
            repositoryEventMember.Delete(removingMember);
        }       

        public IEnumerable<EventMembers> GetAll()
        {
            return _context.EventMembers;
        }

        public EventMembers GetById(int id)
        {
            return _context.EventMembers.Find(id);
        }       

        public void Delete(int id)
        {
            var eventMember = _context.EventMembers.Find(id);
            if (eventMember != null)
            {
                _context.EventMembers.Remove(eventMember);
                _context.SaveChanges();
            }
        }
    }
}
