using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Api.Services;

namespace Vavatech.Shopper.Api.Startup
{

    public static class BasicEndpoints
    {
        public static WebApplication MapBasicEndpoints(this WebApplication app)
        {
            app.MapGet("/Ping", () => "Pong");


            // .NET 6
            var handle = () => "Hello from lambda variable!";

            string LocalFunction() => "Hello from local function";


            // dodatek: Rainbow Braces

            // Basic Endpoints
            app.MapGet("/", () => "Hello .NET Core 6!");
            app.MapGet("/hello", handle);
            app.MapGet("/function", LocalFunction);
            app.MapGet("/static", Handlers.Hello);

            // Przekazywanie parametrów

            // Route Params
            app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");



            // GET /api/products?onstock=true&from=100&to=200
            //app.MapGet("/api/products", (ProductQueryRecordParams parameters) => $"Hello Products {parameters.OnStock} {parameters.From} {parameters.To}");

            // GET /api/shops?location={lat}:{lng}
            // od .NET 7 [AsParameters]
            app.MapGet("/api/shops", ([FromQuery] LocationRecord location) => $"Get shop nearly {location.Latitude} {location.Longitude}");

            // GET /greetings/John?color=Red
            app.MapGet("/greetings/{name}", (HttpContext context) =>
            {
                var name = context.Request.RouteValues["name"];
                var color = context.Request.Query["color"];

                string message = $"Hello {name} {color}";

                context.Response.WriteAsync(message);
            });

            // GET /welcome/John?color=Red
            app.MapGet("/welcome/{name}", (HttpRequest request, HttpResponse response) =>
            {
                var name = request.RouteValues["name"];
                var color = request.Query["color"];
                var userAgent = request.Headers["user-agent"];

                string message = $"Hello {name} {color} {userAgent}";

                response.WriteAsync(message);
            });

            app.MapGet("/update", (HttpResponse res) =>
            {
                return Results.Extensions.NotModified();
            });
            

            return app;
        }

    }
}
