namespace Lingva.DataAccessLayer.Repositories
{
    public interface IUnitOfWorkDictionary : IUnitOfWork
    {
        IRepositoryDictionaryRecord DictionaryRecords { get; }
        IRepositoryWord Words { get; }
    }
}