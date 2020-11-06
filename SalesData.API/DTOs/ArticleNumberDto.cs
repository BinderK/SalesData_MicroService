using SalesData.BL.DomainModels;
using System;
using System.Collections.Generic;
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
