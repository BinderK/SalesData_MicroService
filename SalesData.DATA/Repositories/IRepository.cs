using System.Collections.Generic;

namespace SalesData.DATA.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        void Remove(TEntity entity);
    }
}