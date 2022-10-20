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

string jsonplaceholderUrl = builder.Configuration["jsonplaceholder"];

// string nbpApiUrl = builder.Configuration["NbpApi:Url"];

builder.Services.Configure<NbpApiServiceOptions>(builder.Configuration.GetSection("NbpApi"));

Console.WriteLine(jsonplaceholderUrl);

// dotnet add package Refit.HttpClientFactory
builder.Services.AddRefitClient<IJsonPlaceholderService>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri(jsonplaceholderUrl);
    });
   


//builder.Services.AddHttpClient("NBPApi", httpClient =>
//{
//    httpClient.BaseAddress = new Uri("http://api.nbp.pl");
//});

builder.Services.AddHttpClient<NbpApiService>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["NbpApi:Url"]);
});

var app = builder.Build();

app.UseStaticFiles();

app.MapBasicEndpoints();
app.MapCustomerEndpoints();
app.MapReportsEndpoints();
app.MapUserEndpoints();


app.Run();