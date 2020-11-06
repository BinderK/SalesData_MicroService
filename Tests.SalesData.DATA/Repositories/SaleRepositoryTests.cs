using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SalesData.BL.DomainModels;
using SalesData.DATA;
using SalesData.DATA.Repositories;
using System;
using System.Linq;

namespace Tests.SalesData.DATA.Repositories
{
    [TestFixture]
    public class SaleRepositoryTests
    {
        private ApplicationDbContext _context;
        private Repository<Sale> _repository;
        private SaleRepositoryScenario _scenario;
        

        [SetUp]
        public void Setup()
        {
            _scenario = SaleRepositoryScenario.Create();
            var sales = _scenario.GetDbSales();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SaleDataTestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            if (_context.Sales.Count() > 0)
            {
                _context.Sales.RemoveRange(sales);
                _context.SaveChanges();
            }
            _context.Sales.AddRange(sales);
            _context.SaveChanges();

            _repository = new Repository<Sale>(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void Get_Returns_Element_With_Provided_Id_From_The_DbSet()
        {
            var saleId = 1;
            var expectedSale = _scenario.GetDbSaleById(saleId);

            var sale = _repository.Get(saleId);

            Assert.That(sale, Is.EqualTo(expectedSale));
        }

        [Test]
        public void Get_Returns_Null_When_Provided_Id_Was_Not_Found()
        {
            var saleId = 123;

            var sale = _repository.Get(saleId);

            Assert.That(sale, Is.Null);
        }

        [Test]
        public void GetAll_Returns_All_Elements_From_The_DbSet()
        {
            var expectedSales = _scenario.GetDbSales();

            var sales = _repository.GetAll();

            CollectionAssert.AreEquivalent(sales, expectedSales);
        }

        [Test]
        public void Add_Adds_The_Passed_Element_To_The_DbSet()
        {
            var saleId = 123;
            var articleNumber = "NewArticle";
            var price = 850.99;
            var saleDate = new DateTime(2020, 12, 24);
            var newSale = new Sale
            {
                Id = saleId,
                ArticleNumber = ArticleNumber.CreateArticleNumber(articleNumber),
                SalesPrice = price,
                SaleDate = saleDate
            };

            _repository.Add(newSale);

            var insertedSale = _repository.Get(saleId);
            Assert.That(newSale, Is.EqualTo(insertedSale));
        }

        [Test]
        public void Remove_Removes_Passed_Element_From_The_DbSet()
        {
            var saleId = 1;
            var saleToRemove = _scenario.GetDbSaleById(saleId);            

            _repository.Remove(saleToRemove);
            _context.SaveChanges();

            var dbSales = _repository.GetAll();
            Assert.That(dbSales, !Contains.Item(saleToRemove));
        }
    }
}
