using Microsoft.EntityFrameworkCore;
using SalesData.BL.DomainModels;
using SalesData.DATA.Configurations;

namespace SalesData.DATA
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Sale> Sales { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SaleConfiguration.Configure(modelBuilder);
        }
    }
}