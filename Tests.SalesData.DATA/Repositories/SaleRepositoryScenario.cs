using SalesData.BL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.SalesData.DATA.Repositories
{
    internal sealed class SaleRepositoryScenario
    {
        private static List<Sale> CreateSalesTestData() => new List<Sale>()
        {
            new Sale{ Id = 50, ArticleNumber = ArticleNumber.CreateArticleNumber("FGDG23456hjkl333"), SaleDate = new DateTime(2020, 01, 01), SalesPrice = 54.25 },
            new Sale{ Id = 1047, ArticleNumber = ArticleNumber.CreateArticleNumber("FGDG23456hjkl333"), SaleDate = new DateTime(2020, 05, 10), SalesPrice = 50 },
            new Sale{ Id = 99, ArticleNumber = ArticleNumber.CreateArticleNumber("FGDG23456hjkl333"), SaleDate = new DateTime(2020, 05, 10), SalesPrice = 52.1 },
            new Sale{ Id = 1, ArticleNumber = ArticleNumber.CreateArticleNumber("ERTZUI654ASD"), SaleDate = new DateTime(2020, 05, 10), SalesPrice = 159.99 },
            new Sale{ Id = 12, ArticleNumber = ArticleNumber.CreateArticleNumber("IKMNGKB122222"), SaleDate = new DateTime(2020, 07, 28), SalesPrice = 9.9 }
        };

        public static SaleRepositoryScenario Create() => new SaleRepositoryScenario();

        private readonly List<Sale> _salesTestData;

        public SaleRepositoryScenario()
        {
            _salesTestData = CreateSalesTestData();
        }

        public List<Sale> GetDbSales()
        {
            return _salesTestData;
        }

        public Sale GetDbSaleById(int id)
        {
            return _salesTestData.First(s => s.Id == id);
        }
    }
}
