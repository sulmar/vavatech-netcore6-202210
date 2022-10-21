using System.Diagnostics;

namespace Vavatech.Shopper.Api.Middlewares
{
    public class DebugLoggerMiddleware
    {
        private readonly RequestDelegate next;

        public DebugLoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Debug.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Connection.RemoteIpAddress}");

            await next(context);

            Debug.WriteLine($"{context.Response.StatusCode}");
        }
    }
}
