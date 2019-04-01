namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkUser : IUnitOfWork
    {
        IRepositoryUser Users { get; }
    }
}