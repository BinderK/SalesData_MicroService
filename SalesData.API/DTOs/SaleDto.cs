using SalesData.BL.DomainModels;

namespace SalesData.API.DTOs
{
    public class SaleDto
    {
        public SaleDto(int id, ArticleNumber articleNumber, double salesPrice)
        {
            Id = id;
            ArticleNumber = articleNumber;
            SalesPrice = salesPrice;
        }

        public SaleDto(Sale sale) : this(sale.Id, sale.ArticleNumber, sale.SalesPrice)
        {
        }

        public int Id { get; }

        public ArticleNumber ArticleNumber { get; }

        public double SalesPrice { get; }
    }
}
