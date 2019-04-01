using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryLanguage : Repository<Language>, IRepositoryLanguage
    {
        public RepositoryLanguage(DictionaryContext context)
            : base(context)
        {
        }
    }
}