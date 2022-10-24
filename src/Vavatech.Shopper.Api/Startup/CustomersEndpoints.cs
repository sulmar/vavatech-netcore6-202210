using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Api.Extensions;

namespace Vavatech.Shopper.Api.Startup
{
    public static class CustomersEndpoints
    {
        public static WebApplication MapCustomersEndpoints(this WebApplication app)
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
                .WithName("GetCustomerById")
                .Produces<Customer>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .RequireAuthorization("Adult");
                

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
            app.MapPost("/api/customers", (
                Customer customer,
                ICustomerRepository repository,
                IValidator<Customer> validator) =>
            {
                var validationResult = validator.Validate(customer);

                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

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

    }
}
