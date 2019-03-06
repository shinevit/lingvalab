﻿using Lingva.DataAccessLayer.Context;
using Lingva.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public class RepositoryWord : Repository<Word>, IRepositoryWord
    {
        public RepositoryWord(DictionaryContext context) : base(context)
        {

        }       
    }
}
