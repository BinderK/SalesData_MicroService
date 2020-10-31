using SalesData.BL.DomainModels;
using SalesData.DATA.Repositories;
using System.Threading.Tasks;

namespace SalesData.DATA
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Sale> Sales { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            Sales = new Repository<Sale>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
