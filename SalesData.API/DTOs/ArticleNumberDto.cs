using Newtonsoft.Json;
using SalesData.BL.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesData.API.DTOs
{
    public class ArticleNumberDto
    {
        public ArticleNumberDto(string value)
        {
            Value = value;
        }

        [JsonProperty(Required = Required.Always)]
        [MinLength(1)]
        [MaxLength(ArticleNumber.MAXIMUM_LENGTH)]
        [RegularExpression(ArticleNumber.ALPHANUMERIC_PATTERN,
            ErrorMessage = "Article number must only consist of letters and numbers.")]
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object other)
        {
            if (!(other is ArticleNumber otherArticleNumber))
            {
                return false;
            }

            return Value.Equals(otherArticleNumber.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
