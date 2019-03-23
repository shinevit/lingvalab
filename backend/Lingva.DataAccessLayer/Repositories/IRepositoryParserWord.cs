using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepositoryParserWord : IRepository<ParserWord>
    {
        void CreateOrUpdate(ParserWord word);

        bool Exists(string wordName);

        bool Any();
    }
}
