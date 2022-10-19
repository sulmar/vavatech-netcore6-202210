using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Domain;
using Vavatech.Shopper.Infrastructure;

// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // rejestracja us³ugi w kontenerze wstrzykiwania zale¿noœci (Dependency Injection)
var app = builder.Build();

// .NET 6
var handle = () => "Hello from lambda variable!";

string LocalFunction() => "Hello from local function";


// dodatek: Rainbow Braces

// Basic Endpoints
app.MapGet("/", () => "Hello .NET Core 6!");
// app.MapGet("/api/customers", () => "Hello Customers!");


app.MapGet("/hello", handle);
app.MapGet("/function", LocalFunction);
app.MapGet("/static", Handlers.Hello);


// Przekazywanie parametrów

// Route Params
app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");

// Regu³y (constrains)
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

// POST /api/customers
// Content-Type: application/json
app.MapPost("/api/customers", (Customer customer, ICustomerRepository repository) =>
{
    repository.Add(customer);

    // z³a praktyka
    //   return Results.Created($"http://localhost:5000/api/customers/{customer.Id}", customer);

    return Results.CreatedAtRoute("GetCustomerById", new { customer.Id }, customer);

});


app.Run();