using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesData.DATA;
using System;
using System.Linq;

namespace SalesData.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            UpdateDatabase(host.Services);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            try
            {
                using var services = serviceProvider.CreateScope();
                using var context = services.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (context is InMemoryDbContext)
                {
                    return;
                }

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
