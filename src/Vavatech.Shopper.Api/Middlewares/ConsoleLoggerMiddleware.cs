using System.Runtime.CompilerServices;

namespace Vavatech.Shopper.Api.Middlewares
{
    public static class ConsoleLoggerMiddlewareExtensions
    {
        public static WebApplication UseLogger(this WebApplication app)
        {
            app.UseMiddleware<ConsoleLoggerMiddleware>();
            app.UseMiddleware<DebugLoggerMiddleware>();

            return app;
        }

        public class ConsoleLoggerMiddleware
    {
        private readonly RequestDelegate next;

        public ConsoleLoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Connection.RemoteIpAddress}");

            await next(context);

            Console.WriteLine($"{context.Response.StatusCode}");
        }
    }
}
