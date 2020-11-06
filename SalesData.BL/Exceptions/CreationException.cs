using System;

namespace SalesData.BL.Exceptions
{
    public class CreationException : Exception
    {
        public CreationException(string message) : base(message)
        {
        }
    }
}
