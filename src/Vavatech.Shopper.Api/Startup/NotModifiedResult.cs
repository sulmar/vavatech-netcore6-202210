namespace Vavatech.Shopper.Api.Startup
{

    public static class ResultsExtensions
    {
        public static IResult NotModified(this IResultExtensions resultExtensions)
        {
            ArgumentNullException.ThrowIfNull(resultExtensions, nameof(resultExtensions));

            return new NotModifiedResult();
        }
    }

    public class NotModifiedResult : IResult
    {
        public Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 304;

            return Task.CompletedTask;
        }
    }
}
