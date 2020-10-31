using System;
using System.Text.RegularExpressions;

namespace SalesData.BL.DomainModels
{
    public class ArticleNumber
    {
        private const int MAXIMUM_LENGTH = 32;

        private ArticleNumber(string articleNumber)
        {
            Value = articleNumber;
        }

        public string Value { get; }

        public static ArticleNumber CreateArticleNumber(string articleNumber)
        {
            if (!IsAlphaNumeric(articleNumber))
            {
                throw new Exception("Article number must only consist of letters and numbers.");
            }

            if (ExceedsMaximumLength(articleNumber))
            {
                throw new Exception($"Article number exceeds maximum length of {MAXIMUM_LENGTH}");
            }

            return new ArticleNumber(articleNumber);
        }

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

        private static bool IsAlphaNumeric(string value)
        {
            string pattern = "^[a-zA-Z0-9]*$";
            return Regex.IsMatch(value, pattern);
        }

        private static bool ExceedsMaximumLength(string value)
        {
            return value.Length > MAXIMUM_LENGTH;
        }
    }
}
