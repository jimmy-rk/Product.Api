using Microsoft.Extensions.Logging;
using System;

namespace Product.Api.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogServiceResponse(
            this ILogger logger,
            string message,
            bool serviceResponseSuccess,
            object responseData
            )
        {
            if (serviceResponseSuccess)
                logger.LogInformation($"Success: {message} - {{@ResponseData}}", responseData);
            else
                logger.LogWarning($"Warning: {message} - {{@ResponseData}}", responseData);
        }

        public static void LogException(
            this ILogger logger,
            string message,
            Exception exception
            )
        {
            logger.LogError(exception, $"Exception: {message}");
        }


    }
}
