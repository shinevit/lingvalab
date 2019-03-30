﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lingva.DataAccessLayer.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        void CreateRange(IEnumerable<TEntity> items);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
