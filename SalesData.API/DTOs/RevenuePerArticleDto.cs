using System;

namespace SalesData.API.DTOs
{
    public class RevenuePerArticleDto
    {
        public RevenuePerArticleDto(ArticleNumberDto articleNumber, int salesNumber, double revenue)
        {
            ArticleNumber = articleNumber;
            SalesNumber = salesNumber;
            Revenue = revenue;
        }

        public ArticleNumberDto ArticleNumber { get; }

        public int SalesNumber { get; }

        public double Revenue { get; }

        public override bool Equals(object other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return ArticleNumber.GetHashCode() 
                + SalesNumber.GetHashCode()
                + Math.Round(Revenue, 2).GetHashCode();
        }
    }
}
