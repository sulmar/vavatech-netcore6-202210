using System.Net.Mime;
using Vavatech.Shopper.Api.Services;

namespace Vavatech.Shopper.Api.Startup
{
    public static class UsersEndpoints
    {
        public static WebApplication MapUsersEndpoints(this WebApplication app)
        {
            // Wstrzykiwanie HttpClient za pomocą fabryki
            //app.MapGet("/api/users", async (IHttpClientFactory factory) =>
            //{
            //    var client = factory.CreateClient("JsonPlaceholder");

            //    var stream = await client.GetStreamAsync("/users");

            //    return Results.Stream(stream, MediaTypeNames.Application.Json);
            //});

            app.MapGet("/api/users", async (IJsonPlaceholderService client) =>
            {
                var stream = await client.GetUsers();

                return Results.Stream(stream, MediaTypeNames.Application.Json);
            });

            app.MapGet("/api/users/{id}", async (IJsonPlaceholderService client, int id) =>
            {
                var stream = await client.GetUser(id);

                return Results.Stream(stream, MediaTypeNames.Application.Json);
            });


            return app;


        }
    }
}
