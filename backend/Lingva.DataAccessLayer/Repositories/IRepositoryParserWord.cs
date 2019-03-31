using Lingva.DataAccessLayer.Entities;

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