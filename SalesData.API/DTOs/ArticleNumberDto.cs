using SalesData.BL.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesData.API.DTOs
{
    public class ArticleNumberDto
    {
        public ArticleNumberDto(string articleNumber)
        {
            Value = articleNumber;
        }

        public string Value { get; set; }
    }
}
