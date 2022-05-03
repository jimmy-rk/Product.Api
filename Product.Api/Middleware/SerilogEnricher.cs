using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Product.Api.Middleware
{
    public class SerilogEnricher
    {
        private readonly RequestDelegate _next;

        public SerilogEnricher(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var remoteIp = context.Connection.RemoteIpAddress.ToString() ?? "unknown";

            using (LogContext.PushProperty("Username", "Jimmy RK"))
            using (LogContext.PushProperty("RemoteIP", remoteIp))
            {
                await _next(context);
            }
        }

    }
}
