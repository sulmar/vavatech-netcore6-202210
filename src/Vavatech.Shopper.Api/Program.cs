using FluentValidation;
using Refit;
using Vavatech.Shopper.Api.Services;
using Vavatech.Shopper.Domain.Validators;


// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // rejestracja us³ugi w kontenerze wstrzykiwania zale¿noœci (Dependency Injection)

builder.Services.AddTransient<IValidator<Customer>, CustomerValidator>();

//builder.Services.AddHttpClient("JsonPlaceholder", httpClient=>
//{
//    httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
//});

//builder.Services.AddHttpClient<JsonPlaceholderService>(httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
//});


// dotnet add package Refit.HttpClientFactory
builder.Services.AddRefitClient<IJsonPlaceholderService>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
    });
   


//builder.Services.AddHttpClient("NBPApi", httpClient =>
//{
//    httpClient.BaseAddress = new Uri("http://api.nbp.pl");
//});

builder.Services.AddHttpClient<NbpApiService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("http://api.nbp.pl");
});

var app = builder.Build();

app.UseStaticFiles();

app.MapBasicEndpoints();
app.MapCustomerEndpoints();
app.MapReportsEndpoints();
app.MapUserEndpoints();


app.Run();