using Lingva.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepositoryParserWord : IRepository<ParserWord>
    {
        bool InsertOrUpdate(ParserWord word);

        bool Any();

        bool Exists(string name);

        void CreateRange(IEnumerable<ParserWord> words);
    }
}