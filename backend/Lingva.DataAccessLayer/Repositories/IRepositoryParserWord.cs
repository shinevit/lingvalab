using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepositoryParserWord : IRepository<ParserWord>
    {
        void InsertOrUpdate(ParserWord word);

        bool Exists(string wordName);

        bool Any();

        void CreateRange(IEnumerable<ParserWord> words);
    }
}
