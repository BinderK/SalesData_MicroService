using Microsoft.EntityFrameworkCore;

namespace SalesData.DATA
{
    public class MsSqlDbContext : ApplicationDbContext
    {
        public MsSqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}