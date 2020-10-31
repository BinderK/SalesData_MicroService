using Microsoft.AspNetCore.Mvc;
using SalesData.API.DTOs;
using System.Collections.Generic;

namespace SalesData.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<SaleDto> Get()
        { 
        
        }

        [HttpPost]
        public void Post(SaleDto saleDto)
        {

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
