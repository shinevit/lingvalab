using Lingva.BusinessLayer.Contracts;
using Lingva.DataAccessLayer.Entities;
using Lingva.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingva.BusinessLayer.Services
{
    public class GroupsCollectionService : IGroupsCollectionService
    {
        private readonly IUnitOfWorkGroupsCollection _unitOfWork;

        public GroupsCollectionService(IUnitOfWorkGroupsCollection unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public void AddGroup(Group group)
        {
            //if (!ExistGroup(group))//??
            //{
                _unitOfWork.Groups.Create(group);
                _unitOfWork.Save();
            //}
        }

        public void UpdateGroup(int id, Group group)
        {
            Group myEvent = _unitOfWork.Groups.Get(id);
            //myEvent.Translation = myEventUpdate.Translation;//??
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

        //private bool ExistGroup(Group myEvent)//??
        //{
            //return _unitOfWork.DictionaryRecords.Get(c => c.UserId == myEvent.UserId
            //                            && c.WordName == myEvent.WordName
            //                            && c.Translation == myEvent.Translation
            //                            && c.LanguageName == myEvent.LanguageName) != null;
        //}
    }
}

