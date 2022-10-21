using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Refit;
using Serilog;
using Serilog.Formatting.Compact;
using System.Diagnostics;
using Vavatech.Shopper.Api.HealthChecks;
using Vavatech.Shopper.Api.Middlewares;
using Vavatech.Shopper.Api.Services;
using Vavatech.Shopper.Domain.Validators;


// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder(args);

// builder.Logging.AddJsonConsole();

// dotnet add package Serilog.AspNetCore
builder.Host.UseSerilog((context, logger) =>
{
    // Sinks
    logger.WriteTo.Console();
    logger.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
    logger.WriteTo.File(new CompactJsonFormatter(), "logs/log.json");

    // dotnet add package Serilog.Sinks.Seq
    logger.WriteTo.Seq("http://localhost:5341");

});


// SETX ASPNETCORE_ENVIRONMENT="Testing"
string environmentName = builder.Environment.EnvironmentName;

if (builder.Environment.IsEnvironment("Testing"))
{

}

// Domyœlni providerzy
builder.Configuration.AddJsonFile("appsettings.json", optional: false);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddUserSecrets<Program>();

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

// builder.Configuration.AddKeyPerFile("key-name");

string googleSecretKey = builder.Configuration["GoogleSecretKey"];

string secretContent = builder.Configuration["key-name"];

builder.Configuration.AddEnvironmentVariables("API"); // --API_NbpApi = USD

// Hosting
// https://weblog.west-wind.com/posts/2016/Jun/06/Publishing-and-Running-ASPNET-Core-Applications-with-IIS#running-iis-as-a-development-server-no

builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // rejestracja us³ugi w kontenerze wstrzykiwania zale¿noœci (Dependency Injection)
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IPaymentRepository, InMemoryPaymentRepository>();

builder.Services.AddTransient<IValidator<Customer>, CustomerValidator>();
builder.Services.AddTransient<IDocumentService, PdfDocumentService>();

builder.Services.AddTransient<IMessageSender, FakeMessageSender>();


builder.Services.AddMediatR(typeof(Program));

// dotnet add package AspNetCore.HealthChecks.UI
// dotnet add package AspNetCore.HealthChecks.UI.InMemory.Storage
builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(15);
    options.AddHealthCheckEndpoint("Vavatech API", "/health");
}).AddInMemoryStorage();


// Health Check
builder.Services.AddHealthChecks()
    .AddCheck("Ping", () => HealthCheckResult.Healthy())
    .AddCheck("Random", () =>
    {
        if (DateTime.Now.Minute % 2 == 0)
        {
            return HealthCheckResult.Healthy();
        }
        else
            return HealthCheckResult.Unhealthy();
    })
    .AddCheck<NbpApiHealthCheck>("NBPApi");
    


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


builder.Services.AddControllers();

// dotnet add package FluentValidation.AspNetCore
builder.Services.AddFluentValidationAutoValidation();



var app = builder.Build();

// Middleware (warstwa poœrednia)

// Console Logger Middleware

#region Middleware Lambda

//app.Use(async (context, next) =>
//{
//    Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Connection.RemoteIpAddress}");

//    await next();

//    Console.WriteLine($"{context.Response.StatusCode}");

//});


// Debug Logger Middleware

//app.Use(async (context, next) =>
//{
//    Debug.WriteLine($"{context.Request.Method} {context.Request.Path}");

//    await next();

//    Debug.WriteLine($"{context.Response.StatusCode}");
//});


//// Secret Key Middleware
//app.Use(async (context, next) =>
//{
//    if (context.Request.Headers.TryGetValue("X-Secret-Key", out var secretKey) && secretKey == "123")
//    {
//        await next();
//    }
//    else
//    {
//        context.Response.StatusCode = StatusCodes.Status403Forbidden;
//    }
//});
#endregion

// app.UseMiddleware<ConsoleLoggerMiddleware>();
// app.UseMiddleware<DebugLoggerMiddleware>();
// app.UseMiddleware<SecretKeyMiddleware>();

app.UseLogger();
// app.UseSecretKey();

app.UseStaticFiles();
app.MapEndpoints();
app.MapControllers();


app.MapHealthChecks("/health", new HealthCheckOptions
{
    //  dotnet add package AspNetCore.HealthChecks.UI.Client
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


// /healthchecks-ui
app.MapHealthChecksUI();



app.Run();