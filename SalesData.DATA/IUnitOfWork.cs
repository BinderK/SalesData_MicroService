using SalesData.BL.DomainModels;
using SalesData.DATA.Repositories;
using System;
using System.Threading.Tasks;

namespace SalesData.DATA
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sale> Sales { get; }

        int Complete();

        Task<int> CompleteAsync();
    }
}