﻿using SalesData.BL.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SalesData.BL.DomainModels
{
    public class ArticleNumber
    {
        public const int MAXIMUM_LENGTH = 32;
        public const string ALPHANUMERIC_PATTERN = "^[a-zA-Z0-9]*$";

        private ArticleNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ArticleNumber CreateArticleNumber(string articleNumber)
        {
            if (!IsAlphaNumeric(articleNumber))
            {
                throw new CreationException("Article number must only consist of letters and numbers.");
            }

            if (ExceedsMaximumLength(articleNumber))
            {
                throw new CreationException($"Article number exceeds maximum length of {MAXIMUM_LENGTH}");
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
            return Regex.IsMatch(value, ALPHANUMERIC_PATTERN);
        }

        private static bool ExceedsMaximumLength(string value)
        {
            return value.Length > MAXIMUM_LENGTH;
        }
    }
}
