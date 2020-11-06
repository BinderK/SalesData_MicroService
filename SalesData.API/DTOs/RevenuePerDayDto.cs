using System;

namespace SalesData.API.DTOs
{
    public class RevenuePerDayDto
    {
        public RevenuePerDayDto(DateTime saleDate, int salesNumber, double revenue)
        {
            SaleDate = saleDate;
            SalesNumber = salesNumber;
            Revenue = revenue;
        }

        public DateTime SaleDate { get; }

        public int SalesNumber { get; }

        public double Revenue { get; }

        public override bool Equals(object other)
        {
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return SaleDate.GetHashCode();
        }
    }
}
