using Microsoft.EntityFrameworkCore;

namespace SalesData.DATA
{
    public class InMemoryDbContext : ApplicationDbContext
    {
        public InMemoryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}