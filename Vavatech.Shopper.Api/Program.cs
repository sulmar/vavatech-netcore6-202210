using Vavatech.Shopper.Api;

var app = WebApplication.Create();

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
app.MapGet("/api/customers/{id:int}", (int id) => $"Hello Customer Id = {id}");
app.MapGet("/api/customers/{name}", (string name) => $"Hello Customer {name}");
app.MapGet("/api/customers/{customerId:int}/orders/{orderId:int:min(3000)}", (int customerId, int orderId) => $"Hello Order {orderId} for Customer Id = {customerId}");

// GET /api/customers/01-001
app.MapGet("/api/customers/{postalcode:regex(^\\d{{2}}-\\d{{3}}$)}", (string postalcode) => $"Hello Customers from Postal code {postalcode}");

// GET /api/customers/{customerId:int}/orders/2022
// GET /api/customers/{customerId:int}/orders/2022/10
// GET /api/customers/{customerId:int}/orders/2022/10/19
app.MapGet("/api/customers/{customerId:int}/orders/{*period}", (int customerId, string period) => $"Hello {period} for {customerId}");

// Query String
// GET /api/customers?city={city}&street={street}
app.MapGet("/api/customers", (string? city, string? street) => $"Hello Customer from {city} {street}");

// GET /api/products?onstock=true&from=100&to=200


app.Run();