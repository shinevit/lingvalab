using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepositorySubtitleRow : IRepository<SubtitleRow>
    {
        void InsertOrUpdate(SubtitleRow subtitle);

        bool Any();

        bool Exists(string value);
    }
}