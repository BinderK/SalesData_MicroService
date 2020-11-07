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

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            return query.ToList();
        }
        
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}
