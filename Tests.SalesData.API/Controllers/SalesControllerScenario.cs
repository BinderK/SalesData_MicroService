using AutoMapper;
using Moq;
using SalesData.API.DTOs;
using SalesData.API.MappingProfiles;
using SalesData.BL.DomainModels;
using SalesData.DATA;
using SalesData.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.SalesData.API.Controllers
{
    internal sealed class SalesControllerScenario
    {
        private static List<Sale> CreateSalesTestData() => new List<Sale>()
        {
            new Sale{ Id = 50, ArticleNumber = ArticleNumber.CreateArticleNumber("FGDG23456hjkl333"), SaleDate = new DateTime(2020, 01, 01), SalesPrice = 54.25 },
            new Sale{ Id = 1047, ArticleNumber = ArticleNumber.CreateArticleNumber("FGDG23456hjkl333"), SaleDate = new DateTime(2020, 05, 10), SalesPrice = 50 },
            new Sale{ Id = 99, ArticleNumber = ArticleNumber.CreateArticleNumber("FGDG23456hjkl333"), SaleDate = new DateTime(2020, 05, 10), SalesPrice = 52.1 },
            new Sale{ Id = 1, ArticleNumber = ArticleNumber.CreateArticleNumber("ERTZUI654ASD"), SaleDate = new DateTime(2020, 05, 10), SalesPrice = 159.99 },
            new Sale{ Id = 12, ArticleNumber = ArticleNumber.CreateArticleNumber("IKMNGKB122222"), SaleDate = new DateTime(2020, 07, 28), SalesPrice = 9.9 }
        };

        private static List<SaleDto> CreateSalesDtoTestData() => new List<SaleDto>()
        {
            new SaleDto(50, new ArticleNumberDto("FGDG23456hjkl333"), 54.25, new DateTime(2020, 01, 01)),
            new SaleDto(1047, new ArticleNumberDto("FGDG23456hjkl333"), 50, new DateTime(2020, 05, 10)),
            new SaleDto(99, new ArticleNumberDto("FGDG23456hjkl333"), 52.1, new DateTime(2020, 05, 10)),
            new SaleDto(1, new ArticleNumberDto("ERTZUI654ASD"), 159.99, new DateTime(2020, 05, 10)),
            new SaleDto(12, new ArticleNumberDto("IKMNGKB122222"), 9.9, new DateTime(2020, 07, 28)),
        };

        private readonly List<Sale> _salesTestData;
        private readonly List<SaleDto> _salesDtoTestData;
        private Mock<IRepository<Sale>> _salesRepository;

        public static SalesControllerScenario Create() => new SalesControllerScenario();

        public SalesControllerScenario()
        {
            _salesTestData = CreateSalesTestData();
            _salesDtoTestData = CreateSalesDtoTestData();
            Mapper = SetupMapper();
            UnitOfWork = SetupUnitOfWork();
        }

        public IMapper Mapper { get; }

        public IUnitOfWork UnitOfWork { get; }

        public List<SaleDto> GetSaleDtos()
        {
            return _salesDtoTestData;
        }

        public List<Sale> GetDbSales()
        {
            return _salesTestData;
        }

        public List<SalesPerDayDto> GetExpectedSalesPerDayDtos()
        {
            return new List<SalesPerDayDto>
            {
                new SalesPerDayDto(new DateTime(2020, 01, 01), 1),
                new SalesPerDayDto(new DateTime(2020, 05, 10), 3),
                new SalesPerDayDto(new DateTime(2020, 07, 28), 1),
            };
        }

        public List<RevenuePerDayDto> GetExpectedRevenuePerDayDtos()
        {
            return new List<RevenuePerDayDto>
            {
                new RevenuePerDayDto(new DateTime(2020, 01, 01), 1, 54.25),
                new RevenuePerDayDto(new DateTime(2020, 05, 10), 3, 262.09),
                new RevenuePerDayDto(new DateTime(2020, 07, 28), 1, 9.9),
            };
        }

        public List<RevenuePerArticleDto> GetExpectedRevenuePerArticleNumberDtos()
        {
            return new List<RevenuePerArticleDto>
            {
                new RevenuePerArticleDto(new ArticleNumberDto("FGDG23456hjkl333"), 3, 156.35),
                new RevenuePerArticleDto(new ArticleNumberDto("ERTZUI654ASD"), 1, 159.99),
                new RevenuePerArticleDto(new ArticleNumberDto("IKMNGKB122222"), 1, 9.9)
            };
        }

        public object GetSaleDto(int saleId)
        {
            return _salesDtoTestData.First(s => s.Id == saleId);
        }

        private IMapper SetupMapper()
        {
            return new MapperConfiguration(config => config.AddProfile(new SaleProfile())).CreateMapper();
        }

        private IUnitOfWork SetupUnitOfWork()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            _salesRepository = SetupRepositoryMock();
            unitOfWorkMock.SetupGet(u => u.Sales).Returns(_salesRepository.Object);

            return unitOfWorkMock.Object;
        }

        private Mock<IRepository<Sale>> SetupRepositoryMock()
        {
            var salesRepository = new Mock<IRepository<Sale>>();
            salesRepository.Setup(r => r.GetAll()).Returns(_salesTestData);
            salesRepository.Setup(r => r.Add(It.IsAny<Sale>())).Callback((Sale sale) =>
            {
                sale.Id = 123;
                sale.SaleDate = new DateTime(2020, 12, 24);
                _salesTestData.Add(sale);
            });
            salesRepository.Setup(r => r.Get(It.IsAny<int>())).Returns((int saleId) => _salesTestData.FirstOrDefault(s => s.Id == saleId));
            salesRepository.Setup(r => r.Remove(It.IsAny<Sale>())).Callback((Sale sale) => _salesTestData.Remove(sale));

            return salesRepository;
        }
    }
}
