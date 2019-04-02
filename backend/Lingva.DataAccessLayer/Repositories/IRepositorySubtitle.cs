using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IRepositorySubtitle : IRepository<Subtitle>
    {
        int? Get(string path);
    }
}