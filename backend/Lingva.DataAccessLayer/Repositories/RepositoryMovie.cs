using System;
using System.Collections.Generic;
using System.Text;
using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryMovie : Repository<Movie>, IRepositoryMovie
    {
        public RepositoryMovie(DictionaryContext context) : base(context)
        {

        }
    }
}
