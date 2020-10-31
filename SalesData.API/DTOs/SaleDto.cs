using Newtonsoft.Json;
using SalesData.BL.DomainModels;

namespace SalesData.API.DTOs
{
    public class SaleDto
    {
        [JsonConstructor]
        public SaleDto(int id, ArticleNumberDto articleNumber, double salesPrice)
        {
            Id = id;
            ArticleNumber = articleNumber;
            SalesPrice = salesPrice;
        }

        public SaleDto(Sale sale) 
            : this(sale.Id, new ArticleNumberDto(sale.ArticleNumber.Value), sale.SalesPrice)
        {
        }

        public int Id { get; }

        public ArticleNumberDto ArticleNumber { get; }

        public double SalesPrice { get; }
    }
}
