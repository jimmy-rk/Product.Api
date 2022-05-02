using System;

namespace Product.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrInvalidGuid(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return true;
            return !Guid.TryParse(input, out Guid guid);    
        }
    }
}
