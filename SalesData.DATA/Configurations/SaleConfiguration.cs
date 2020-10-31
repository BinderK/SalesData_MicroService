using Microsoft.EntityFrameworkCore;
using SalesData.BL.DomainModels;

namespace SalesData.DATA.Configurations
{
    public static class SaleConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(c =>
            {
                c.HasKey(s => s.Id);
                c.Property(s => s.ArticleNumber).HasConversion(
                    a => a.ToString(), 
                    a => ArticleNumber.CreateArticleNumber(a));                 
            });

        }
    }
}
