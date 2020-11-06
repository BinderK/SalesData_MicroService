using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SalesData.API.Controllers;
using SalesData.API.DTOs;
using SalesData.BL.DomainModels;
using SalesData.DATA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.SalesData.API.Controllers
{
    [TestFixture]
    public class SalesControllerTests
    {
        private SalesController _salesController;
        private SalesControllerScenario _scenario;

        [SetUp]
        public void Setup()
        {
            _scenario = SalesControllerScenario.Create();
            _salesController = new SalesController(_scenario.UnitOfWork, _scenario.Mapper);
        }

        [Test]
        public void Get_Returns_All_SalesDtos_From_DataSet()
        {
            var expectedSales = _scenario.GetSaleDtos();

            var getResult = _salesController.Get() as OkObjectResult;
            var salesDto = getResult.Value as IEnumerable<SaleDto>;

            CollectionAssert.AreEqual(salesDto, expectedSales);
        }

        [Test]
        public void GetById_Returns_SalesDto_With_The_Passed_Id()
        {
            var saleId = 99;
            var expectedSale = _scenario.GetSaleDto(saleId);

            var getResult = _salesController.GetById(saleId) as OkObjectResult;
            var saleDto = getResult.Value as SaleDto;

            Assert.That(saleDto, Is.EqualTo(expectedSale));
        }

        [Test]
        public void GetById_Returns_Not_Found_Result_If_Id_Does_Not_Exist()
        {
            var saleId = 123;

            var getResult = _salesController.GetById(saleId) as NotFoundObjectResult;
            var errorMessage = getResult.Value as string;

            Assert.That(getResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            Assert.That(errorMessage, Is.EqualTo($"Sale with id {saleId} not found"));
        }

        [Test]
        public void Post_Adds_New_Object_To_DataSet_And_Returns_New_Object()
        {
            var saleId = 123;
            var articleNumber = "NewArticle";
            var price = 850.99;
            var saleDate = new DateTime(2020, 12, 24);
            var newSale = new SaleDto(0, new ArticleNumberDto(articleNumber), price, DateTime.MinValue);
            var expectedSaleDto = new SaleDto(saleId, new ArticleNumberDto(articleNumber), 850.99, saleDate);
            var expectedSale = new Sale {
                Id = saleId,
                ArticleNumber = ArticleNumber.CreateArticleNumber(articleNumber),
                SalesPrice = price,
                SaleDate = saleDate
            };

            var postResult = _salesController.Post(newSale) as OkObjectResult;
            var sale = postResult.Value as SaleDto;

            var dbData = _scenario.GetDbSales();
            Assert.That(sale, Is.EqualTo(expectedSaleDto));
            Assert.That(dbData, Contains.Item(expectedSale));
        }

        [Test]
        public void Delete_Removes_Object_From_DataSet()
        {
            var saleId = 99;
            var dbData = _scenario.GetDbSales();
            var originalDbCount = dbData.Count();

            var deleteResult = _salesController.Delete(saleId) as OkResult;

            dbData = _scenario.GetDbSales();
            Assert.That(deleteResult.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(dbData.Count(), !Is.EqualTo(originalDbCount));
            Assert.That(dbData.FirstOrDefault(s => s.Id == saleId), Is.Null);
        }

        [Test]
        public void Delete_Returns_Not_Found_Result_If_Id_Does_Not_Exist()
        {
            var saleId = 123;

            var deleteResult = _salesController.Delete(saleId) as NotFoundObjectResult;
            var errorMessage = deleteResult.Value as string;

            Assert.That(deleteResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            Assert.That(errorMessage, Is.EqualTo($"Sale with id {saleId} not found"));
        }

        [Test]
        public void Put_Updates_Object_From_DataSet_And_Returns_Update_Object()
        {           
            var saleId = 123;
            var originalSaleId = 99;
            var articleNumber = "UpdatedArticleNumber";
            var price = 66.66;
            var saleDate = new DateTime(2020, 12, 24);
            var originalSaleDate = new DateTime(2020, 05, 10);
            var updatedSale = new SaleDto(saleId, new ArticleNumberDto(articleNumber), price, saleDate);
            var expectedSaleDto = new SaleDto(originalSaleId, new ArticleNumberDto(articleNumber), price, originalSaleDate);
            var expectedSale = new Sale
            {
                Id = originalSaleId,
                ArticleNumber = ArticleNumber.CreateArticleNumber(articleNumber),
                SalesPrice = price,
                SaleDate = originalSaleDate
            };

            var updateResult = _salesController.Put(originalSaleId, updatedSale) as OkObjectResult;
            var saleDto = updateResult.Value;
            
            var dbData = _scenario.GetDbSales();
            Assert.That(saleDto, Is.EqualTo(expectedSaleDto));
            Assert.That(dbData, Contains.Item(expectedSale));
        }

        [Test]
        public void Put_Returns_Not_Found_Result_If_Id_Does_Not_Exist()
        {
            var saleId = 123;

            var putResult = _salesController.Delete(saleId) as NotFoundObjectResult;
            var errorMessage = putResult.Value as string;

            Assert.That(putResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            Assert.That(errorMessage, Is.EqualTo($"Sale with id {saleId} not found"));
        }

        [Test]
        public void GetSalesPerDay_Returns_Number_Of_Sales_Per_Day()
        {
            var expectedSalesPerDay = _scenario.GetExpectedSalesPerDayDtos();

            var getResult = _salesController.GetSalesPerDay() as OkObjectResult;
            var salesPerDayDtos = getResult.Value as IEnumerable<SalesPerDayDto>;

            CollectionAssert.AreEqual(salesPerDayDtos, expectedSalesPerDay);
        }

        [Test]
        public void GetRevenuePerDay_Returns_Total_Revenue_Per_Day()
        {
            var expectedSales = _scenario.GetExpectedRevenuePerDayDtos();

            var getResult = _salesController.GetRevenuePerDay() as OkObjectResult;
            var salesDto = getResult.Value as IEnumerable<RevenuePerDayDto>;

            CollectionAssert.AreEqual(salesDto, expectedSales);
        }

        [Test]
        public void GetRevenuePerArticle_Returns_Total_Revenue_Per_Article_Number()
        {
            var expectedSales = _scenario.GetExpectedRevenuePerArticleNumberDtos();

            var getResult = _salesController.GetRevenuePerArticle() as OkObjectResult;
            var salesDto = getResult.Value as IEnumerable<RevenuePerArticleDto>;

            CollectionAssert.AreEqual(salesDto, expectedSales);
        }

    }
}
