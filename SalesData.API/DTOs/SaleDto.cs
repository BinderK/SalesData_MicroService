using Newtonsoft.Json;
using SalesData.BL.DomainModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesData.API.DTOs
{
    public class SaleDto
    {
        public SaleDto(int id, ArticleNumberDto articleNumber, double salesPrice, DateTime saleDate)
        {
            Id = id;
            ArticleNumber = articleNumber;
            SalesPrice = salesPrice;
            SaleDate = saleDate;
        }

        public int Id { get; }

        [JsonProperty(Required = Required.Always)]
        public ArticleNumberDto ArticleNumber { get; }

        [JsonProperty(Required = Required.Always)]
        public double SalesPrice { get; }

        public DateTime SaleDate { get; }

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
