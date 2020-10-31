using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SalesData.DATA.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        int Count(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string includeProperties = "");
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        TEntity Get(long id);
        List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        IEnumerable<TEntity> GetAll(string includeProperties = "");
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void RemoveRange(Expression<Func<TEntity, bool>> predicate);
    }
}