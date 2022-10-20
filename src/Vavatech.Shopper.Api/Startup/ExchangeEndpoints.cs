using System.Net.Mime;
using Vavatech.Shopper.Api.Services;

namespace Vavatech.Shopper.Api.Startup
{
    public static class ExchangeEndpoints
    {
        public static WebApplication MapExchangeEndpoints(this WebApplication app)
        {
            app.MapGet("/api/exchanges/{table}/{code}", async (NbpApiService client, string table, string code) =>
            {
                var stream = await client.GetRate(table, code);

                return Results.Stream(stream, MediaTypeNames.Application.Json);
            });

            app.MapGet("/api/exchanges/defalt", async (NbpApiService client) =>
            {
                var stream = await client.GetDefaultRate();

                return Results.Stream(stream, MediaTypeNames.Application.Json);
            });

            return app;

        }

    }
}
