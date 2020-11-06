using System;

namespace SalesData.BL.DomainModels
{
    public class Sale
    {
        public int Id { get; set; }

        public ArticleNumber ArticleNumber { get; set; }

        public double SalesPrice { get; set; }

        public DateTime SaleDate { get; set; }

        public override bool Equals(object other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return Id
                + ArticleNumber.GetHashCode();
        }
    }
}
