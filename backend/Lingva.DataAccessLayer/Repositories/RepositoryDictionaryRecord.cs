//using Lingva.DataAccessLayer.Context;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;

//namespace Lingva.DataAccessLayer.Repositories
//{
//    class RepositoryDictionaryRecord : Repository
//    {
//        private readonly DBContext _context;

//        private DbSet<T> _entities;

//        public Repository(DBContext context)
//        {
//            _context = context;
//            _entities = context.Set<T>();
//        }

//        public IQueryable<T> GetList()
//        {
//            return _entities.AsNoTracking();
//        }

//        public IQueryable<T> GetList(int quantity, Expression<Func<T, bool>> predicator)
//        {
//            return _entities.Where(predicator).Take(quantity).AsNoTracking();
//        }

//        public T Get(int id)
//        {
//            return _entities.Find(id);
//        }

//        public T Get(Expression<Func<T, bool>> predicator)
//        {
//            return _entities.Where(predicator).FirstOrDefault();
//        }

//        public void Create(T entity)
//        {
//            if (entity == null)
//            {
//                throw new ArgumentNullException("Tried to insert null entity!");
//            }

//            _entities.Add(entity);
//        }

//        public void Update(T entity)
//        {
//            _entities.Attach(entity);
//            _context.Entry(entity).State = EntityState.Modified;
//        }

//        public void Delete(T entity)
//        {
//            if (_context.Entry(entity).State == EntityState.Detached)
//            {
//                _entities.Attach(entity);
//            }
//            _entities.Remove(entity);
//        }
//    }
//}
