using FluentValidation;
using Refit;
using Vavatech.Shopper.Api.Services;
using Vavatech.Shopper.Domain.Validators;


// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder(args);

// SETX ASPNETCORE_ENVIRONMENT="Testing"
string environmentName = builder.Environment.EnvironmentName;

if (builder.Environment.IsEnvironment("Testing"))
{

}

// Domyœlni providerzy
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables(); // --ASPNETCORE_NbpApi=USD 
builder.Configuration.AddCommandLine(args); // --NbpApi__Code=USD

// Dodatkowe
builder.Configuration.AddXmlFile("appsettings.xml", optional: true);
builder.Configuration.AddIniFile("appsettings.ini", optional: true);

builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
    {
        { "NbpApi:Code", "CHF" },
        { "NbpApi:Table", "A" },
    }
);

builder.Configuration.AddEnvironmentVariables("API"); // --API_NbpApi = USD

// Hosting
// https://weblog.west-wind.com/posts/2016/Jun/06/Publishing-and-Running-ASPNET-Core-Applications-with-IIS#running-iis-as-a-development-server-no

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