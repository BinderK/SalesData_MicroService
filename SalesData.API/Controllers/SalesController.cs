using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesData.API.DTOs;
using SalesData.BL.DomainModels;
using SalesData.DATA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesData.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;              

        public SalesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all sales items.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SaleDto>), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var sales = _unitOfWork.Sales.GetAll();
            var saleDtos = sales.Select(s => _mapper.Map<SaleDto>(s));

            return new OkObjectResult(saleDtos);
        }

        /// <summary>
        /// Gets the sale item for the provided id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SaleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var sale = _unitOfWork.Sales.Get(id);
            if (sale == null)
            {
                return CreateSaleNotFoundByIdResult(id);
            }

            var saleDto = _mapper.Map<SaleDto>(sale);

            return new OkObjectResult(saleDto);
        }

        /// <summary>
        /// Adds the passed sale item.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SaleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(SaleDto saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            sale.SaleDate = DateTime.UtcNow.Date;
            _unitOfWork.Sales.Add(sale);
            _unitOfWork.Complete();

            return new OkObjectResult(_mapper.Map<SaleDto>(sale));
        }

        /// <summary>
        /// Updates the sale item for the provided id.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SaleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, SaleDto saleDto)
        {
            var sale = _unitOfWork.Sales.Get(id);
            if (sale == null)
            {
                return CreateSaleNotFoundByIdResult(id);
            }

            _mapper.Map(saleDto, sale);
            _unitOfWork.Complete();

            return new OkObjectResult(_mapper.Map<SaleDto>(sale));
        }

        /// <summary>
        /// Removes the sale item for the provided id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var sale = _unitOfWork.Sales.Get(id);
            if (sale == null)
            {
                return CreateSaleNotFoundByIdResult(id);
            }

            _unitOfWork.Sales.Remove(sale);
            _unitOfWork.Complete();

            return new OkResult();
        }

        /// <summary>
        /// Gets the number of sold articles per day.
        /// </summary>
        [HttpGet("day")]
        [ProducesResponseType(typeof(IEnumerable<SalesPerDayDto>), StatusCodes.Status200OK)]
        public IActionResult GetSalesPerDay()
        {
            var sales = _unitOfWork.Sales.GetAll();
            var salesPerDayDtos = sales
                .GroupBy(s => s.SaleDate)
                .Select(g => new SalesPerDayDto(
                    g.Key,
                    g.Count()));

            return new OkObjectResult(salesPerDayDtos);
        }

        /// <summary>
        /// Gets the total revenue per day.
        /// </summary>
        [HttpGet("revenue/day")]
        [ProducesResponseType(typeof(IEnumerable<RevenuePerDayDto>), StatusCodes.Status200OK)]
        public IActionResult GetRevenuePerDay()
        {
            var sales = _unitOfWork.Sales.GetAll();
            var revenuePerDayDtos = sales
                .GroupBy(s => s.SaleDate)
                .Select(g => new RevenuePerDayDto(
                    g.Key,
                    g.Count(),
                    g.Sum(s => s.SalesPrice)));

            return new OkObjectResult(revenuePerDayDtos);
        }

        /// <summary>
        /// Gets the revenue grouped by article.
        /// </summary>
        [HttpGet("revenue/article")]
        [ProducesResponseType(typeof(IEnumerable<RevenuePerArticleDto>), StatusCodes.Status200OK)]
        public IActionResult GetRevenuePerArticle()
        {
            var sales = _unitOfWork.Sales.GetAll();
            var revenuePerArticleDtos = sales
                .GroupBy(s => s.ArticleNumber)
                .Select(g => new RevenuePerArticleDto(
                    new ArticleNumberDto(g.Key.ToString()),
                    g.Count(),
                    g.Sum(s => s.SalesPrice)));

            return new OkObjectResult(revenuePerArticleDtos);
        }

        private NotFoundObjectResult CreateSaleNotFoundByIdResult(int id) => new NotFoundObjectResult($"Sale with id {id} not found");
    }
}
