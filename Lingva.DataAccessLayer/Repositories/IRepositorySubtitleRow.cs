using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepositorySubtitleRow : IRepository<SubtitleRow>
    {
        void InsertOrUpdate(SubtitleRow subtitle);

        bool Any();

        bool Exists(string value);
    }
}
