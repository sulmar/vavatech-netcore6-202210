// var app = WebApplication.Create();

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>(); // rejestracja us³ugi w kontenerze wstrzykiwania zale¿noœci (Dependency Injection)

var app = builder.Build();

app.MapBasicEndpoints();
app.MapCustomerEndpoints();

app.Run();