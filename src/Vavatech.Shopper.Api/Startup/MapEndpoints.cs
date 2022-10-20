﻿using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.IO;
using System.Net.Mime;
using Vavatech.Shopper.Api.Extensions;
using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Api.Startup
{
    public static class MapEndpoints
    {
        public static WebApplication MapBasicEndpoints(this WebApplication app)
        {

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
            app.MapGet("/api/products", (ProductQueryRecordParams parameters) => $"Hello Products {parameters.OnStock} {parameters.From} {parameters.To}");

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


            return app;
        }

        public static WebApplication MapCustomerEndpoints(this WebApplication app)
        {
            // Reguły (constrains)
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0#route-constraints

            //app.MapGet("/api/customers/{id:int}", (int id) => $"Hello Customer Id = {id}");

            /* Standardowa metoda

            app.MapGet("/api/customers/{id:int}", (int id, ICustomerRepository repository) =>
            {
               var customer = repository.Get(id);

                if (customer == null)
                    return Results.NotFound();
                else
                    return Results.Ok(customer);


            })
                .WithName("GetCustomerById");

            */

            // Operator is i ?

            /*
            app.MapGet("/api/customers/{id:int}", (int id, ICustomerRepository repository) => repository.Get(id) is Customer customer 
                ? Results.Ok(customer) 
                : Results.NotFound())
                .WithName("GetCustomerById");
            */

            // Match Patterns
            app.MapGet("/api/customers/{id:int}", (int id, ICustomerRepository repository) => repository.Get(id) switch
            {
                Customer customer => Results.Ok(customer),
                null => Results.NotFound()
            })
                .WithName("GetCustomerById");

            app.MapGet("/api/customers/{name}", (string name) => $"Hello Customer {name}");
            app.MapGet("/api/customers/{customerId:int}/orders/{orderId:int:min(3000)}", (int customerId, int orderId) => $"Hello Order {orderId} for Customer Id = {customerId}");

            // GET /api/customers/01-001
            app.MapGet("/api/customers/{postalcode:regex(^\\d{{2}}-\\d{{3}}$)}", (string postalcode) => $"Hello Customers from Postal code {postalcode}");

            // GET /api/customers/{customerId:int}/orders/2022
            // GET /api/customers/{customerId:int}/orders/2022/10
            // GET /api/customers/{customerId:int}/orders/2022/10/19
            app.MapGet("/api/customers/{customerId:int}/orders/{*period}", (int customerId, string period) => $"Hello {period} for {customerId}");

            // Query Params
            // GET /api/customers?city={city}&street={street}
            app.MapGet("/api/customers", (
                string? city,
                string? street,
                [FromHeader(Name = "user-agent")] string userAgent,
                ICustomerRepository repository) =>
            {
                var customers = repository.Get();

                return customers;
            });


            // POST /api/customers
            // Content-Type: application/json
            app.MapPost("/api/customers", (Customer customer, ICustomerRepository repository) =>
            {
                repository.Add(customer);

                // zła praktyka
                //   return Results.Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

                return Results.CreatedAtRoute("GetCustomerById", new { customer.Id }, customer);

            });


            app.MapPut("/api/customers/{id}", (int id, Customer customer, ICustomerRepository repository) =>
            {
                if (id != customer.Id)
                    return Results.BadRequest();

                repository.Update(customer);

                return Results.NoContent();
            });

            // app.MapMethods("/api/customers/{id}", new[] { "HEAD" }, () => Results.Ok());
            app.MapHead("/api/customers/{id}", () => Results.Ok());

            // app.MapMethods("/api/customers/{id}", new[] { "PATCH" }, () => Results.Ok());
            app.MapPatch("/api/customers/{id}", () => Results.Ok());


            app.MapDelete("api/customers/{id}", (int id, ICustomerRepository repository) => repository.Remove(id));

            return app;
        }

        public static WebApplication MapReportsEndpoints(this WebApplication app)
        {
            // GET api/reports?from=100&to=2000

            app.MapGet("/api/reports", (decimal? from, decimal? to) =>
            {
                string filename = Path.Combine(app.Environment.WebRootPath, "lorem-ipsum.pdf");

                // TODO: generate report
                //  Stream stream = File.OpenRead(filename);

                Stream stream = new MemoryStream();

                // dotnet add package QuestPDF
                // dotnet add package QuestPDF.Previewer
                // https://www.questpdf.com/quick-start
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header()
                            .Text("Hello PDF!")
                            .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);

                                x.Item().Text(Placeholders.LoremIpsum());
                                x.Item().Image(Placeholders.Image(200, 100));
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("Page ");
                                x.CurrentPageNumber();
                            });
                    });
                })
                .GeneratePdf(stream);

                // stream.Position = 0;
                stream.Seek(0, SeekOrigin.Begin);

                return Results.File(stream, MediaTypeNames.Application.Pdf);

            });


            app.MapPost("/api/reports", async (HttpRequest request) =>
            {
                if (!request.HasFormContentType)
                    return Results.BadRequest();

                // var form = request.Form;
                var form = await request.ReadFormAsync();

                var file = form.Files["file1"];

                var customerId = int.Parse(form["customerId"]);

                if (file == null)
                    return Results.BadRequest();

                var path = Path.Combine("uploads", file.FileName);
                using var writeStream = File.OpenWrite(path);
                using var readStream = file.OpenReadStream();

                TextReader reader = new StreamReader(readStream);
                TextWriter writer = new StreamWriter(writeStream);

                //while (reader.Peek() >= 0)
                //{
                //    string? line = reader.ReadLine();
                //    writer.WriteLine(line + "!");
                //}


                // TODO: nie zapisuje do pliku
                // writer.WriteLine("Hello World!");

                await readStream.CopyToAsync(writeStream);


                // writeStream.Position = 0;
                //writeStream.Flush();
                //writeStream.Close();

                return Results.Accepted();

                // return Results.File(writeStream);

            });


           return app;
        }
    }
}