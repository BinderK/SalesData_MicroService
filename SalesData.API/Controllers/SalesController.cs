using Microsoft.AspNetCore.Mvc;
using SalesData.API.DTOs;
using SalesData.BL.DomainModels;
using SalesData.DATA;
using System.Collections.Generic;
using System.Linq;

namespace SalesData.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public SalesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<SaleDto> Get()
        {
            var sales = _unitOfWork.Sales.GetAll();
            return sales.Select(s => new SaleDto(s));
        }

        [HttpPost]
        public void Post(SaleDto saleDto)
        {
            var sale = new Sale()
            {
                ArticleNumber = ArticleNumber.CreateArticleNumber(saleDto.ArticleNumber.Value),
                SalesPrice = saleDto.SalesPrice
            };

            _unitOfWork.Sales.Add(sale);
            _unitOfWork.Complete();
        }

        [HttpPut]
        public void Put(SaleDto saleDto)
        {

        }

        [HttpDelete]
        public void Delete(int id)
        {

        }
    }
}
