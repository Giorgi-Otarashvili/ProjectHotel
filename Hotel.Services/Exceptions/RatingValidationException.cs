using System;

namespace Hotel.Services.Exceptions
{
    public class RatingValidationException : Exception
    {
        public RatingValidationException(string message) : base(message) { }
    }
}