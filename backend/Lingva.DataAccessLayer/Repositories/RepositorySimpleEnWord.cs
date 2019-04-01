using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositorySimpleEnWord : Repository<SimpleEnWord>, IRepositorySimpleEnWord
    {
        public RepositorySimpleEnWord(DictionaryContext context)
            : base(context)
        {

        }
    }
}
