using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
