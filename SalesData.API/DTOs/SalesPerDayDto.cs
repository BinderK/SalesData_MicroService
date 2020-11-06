using System;

namespace SalesData.API.DTOs
{
    public class SalesPerDayDto
    {
        public SalesPerDayDto(DateTime saleDate, int salesNumber)
        {
            SaleDate = saleDate;
            SalesNumber = salesNumber;
        }

        public DateTime SaleDate { get; }

        public int SalesNumber { get; }

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
