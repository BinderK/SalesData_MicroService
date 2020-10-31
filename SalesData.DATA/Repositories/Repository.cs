using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SalesData.DATA.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
       
        public Repository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public TEntity Get(long id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public List<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                    string includeProperties = "")
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
                                        string includeProperties = "")
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            foreach (var includeProperty in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (filter == null)
            {
                return query.FirstOrDefault();
            }
            return query.FirstOrDefault(filter);
        }

        public IEnumerable<TEntity> GetAll(string includeProperties = "")
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            foreach (var includeProperty in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
                                            string includeProperties = "")
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            foreach (var includeProperty in
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (predicate == null)
            {
                return query.ToList();
            }
            return query.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void RemoveRange(Expression<Func<TEntity, bool>> predicate)
        {
            Context.Set<TEntity>().RemoveRange(Context.Set<TEntity>().Where(predicate));
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return Context.Set<TEntity>().Count();
            }
            else
            {
                return Context.Set<TEntity>().Count(predicate);
            }
        }
    }
}
