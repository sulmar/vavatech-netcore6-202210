// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // rejestracja us�ugi w kontenerze wstrzykiwania zale�no�ci (Dependency Injection)

var app = builder.Build();

app.MapBasicEndpoints();
app.MapCustomerEndpoints();

app.Run();