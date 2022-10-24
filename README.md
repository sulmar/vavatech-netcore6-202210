# Tworzenie usług sieciowych w .NET 6

## Podstawy

### Komendy CLI
#### Środowisko
- ``` dotnet --version ``` - wyświetlenie wersji SDK
- ``` dotnet --list-sdks ``` - wyświetlenie listy zainstalowanych SDK
- ``` dotnet new globaljson ``` - utworzenie pliku _global.json_
- ``` dotnet new globaljson --sdk-version {version} ``` - utworzenie pliku _global.json_ i ustawienie wersji SDK

#### Projekt
- ``` dotnet new --list ``` - wyświetlenie listy dostępnych szablonów
- ``` dotnet new {template} ``` - utworzenie nowego projektu na podstawie wybranego szablonu
- ``` dotnet new {template} -o {output} ``` - utworzenie nowego projektu w podanym katalogu
- ``` dotnet build ``` - kompilacja projektu
- ``` dotnet run ``` - uruchomienie projektu
- ``` dotnet watch run ``` - uruchomienie projektu w trybie śledzenia zmian
- ``` dotnet run {app.dll}``` - uruchomienie aplikacji
- ``` dotnet test ``` - uruchomienie testów jednostkowych
- ``` dotnet watch test ``` - uruchomienie testów jednostkowych w trybie śledzenia zmian
- ``` dotnet add {project.csproj} reference {library.csproj} ``` - dodanie odwołania do biblioteki
- ``` dotnet remove {project.csproj} reference {library.csproj} ``` - usunięcie odwołania do biblioteki
- ``` dotnet clean ``` - wyczyszczenie wyniku kompilacji, czyli zawartości folderu pośredniego _obj_ oraz folderu końcowego _bin_

#### Rozwiązanie
- ``` dotnet new sln ``` - utworzenie nowego rozwiązania
- ``` dotnet new sln --name {name} ``` - utworzenie nowego rozwiązania o określonej nazwie
- ``` dotnet sln add {folder}``` - dodanie projektu z folderu do rozwiązania
- ``` dotnet sln remove {folder}``` - usunięcie projektu z folderu z rozwiązania
- ``` dotnet sln add {project.csproj}``` - dodanie projektu do rozwiązania
- ``` dotnet sln remove {project.csproj}``` - usunięcie projektu z rozwiązania

### Konfiguracja

#### Dostawcy
	- AddJsonFile
	- AddCommandLine
	- AddEnvironmentVariables
	- AddXmlFile
	- [AddIniFile](https://minimal-apis.github.io/hello-minimal/#webapplicationbuilder)
	- AddSecretKey
	- AddInMemoryCollection
	- AddKeyPerFile
	- [AddYamlFile](https://github.com/andrewlock/NetEscapades.Configuration#yaml-configuration-provider)	
#### Pobieranie konfiguracji
	- Configuration
	- IOptions

### Środowiska
- Development
- Staging
- Production
- Własne

### [Rejestrowanie logów](https://minimal-apis.github.io/hello-minimal/#logging)

#### Serilog
	- Konsola
	- Plik xml
	- Plik json
	- Seq 

- [OpenTelemetry](https://opentelemetry.io)
- [Jeager](https://www.jaegertracing.io)


### MinimalApi

#### Mapowanie tras

- [Przekazywanie parametrów](https://minimal-apis.github.io/hello-minimal/#parameter-binding)

	- FromRoute
		- BindAsync

	- FromQuery
		- TryParse

	- FromHeader
	- FromBody

- Reguły
	- Wbudowane
	- Własne

### Akcje

- MapGet
- MapPost
- MapDelete
- Results.Forward
- MapMethods
	- HEAD
	- PATCH

### [Odpowiedzi](https://minimal-apis.github.io/hello-minimal/#responses)

- Results.Ok
- Results.NotFound
- Results.CreatedAtRoute
- Results.Text

	- [HTML](https://github.com/sulmar/altkom-netcore6-202210/blob/db5881d84d6cec9d6a6e63fda0c4fbe9b70c9822/src/Altkom.Net6.MinimalApi/Startup/MapEndpoints.cs#L210)

- [Results.Json](https://minimal-apis.github.io/hello-minimal/#json)

- [Results.File](https://minimal-apis.github.io/hello-minimal/#file)

- [Results.Stream](https://minimal-apis.github.io/hello-minimal/#stream)

- [Results.Redirect](https://minimal-apis.github.io/hello-minimal/#redirect)

### Obsługa plików

- [Pliki statyczne](https://minimal-apis.github.io/hello-minimal/#changing-the-web-root)

- Generowanie plików
- Wysyłanie plików

### [OpenAPI](https://minimal-apis.github.io/hello-minimal/#openapi-swagger)

### Walidacja modelu

- [FluentValidation](https://github.com/sulmar/altkom-netcore6-202210/blob/master/src/Altkom.Net6.MinimalApi/Startup/MapEndpoints.cs#L58)

- Results.ValidationProblem

### HttpClient

- [Rerejestrowanie HttpClient](https://github.com/sulmar/dotnet6-minimal-api/blob/master/MinimalApiDemo/Startup/DependencyInjectionSetup.cs#L75)

	- Nazwani
	- [Własna klasa](https://github.com/sulmar/dotnet6-minimal-api/blob/8def5e6cb6c03f8af2bb93c21aa62ca0a1426bd1/MinimalApiDemo/Services/NbpApiClient.cs#L5)

	- [Refit](https://github.com/sulmar/dotnet6-minimal-api/blob/8def5e6cb6c03f8af2bb93c21aa62ca0a1426bd1/MinimalApiDemo/Services/NbpApiClient.cs#L20)

- [Wstrzykiwanie](https://github.com/sulmar/dotnet6-minimal-api/blob/master/MinimalApiDemo/Startup/MapEndpoints.cs#L401)

### Wstrzykiwanie zależności

- Rejestracja usług
- Wstrzykiwanie do metody

### [HealthCheck](https://github.com/sulmar/vavatech-dotnet-6-microservices-202210/blob/master/src/Shopper/CatalogService.Api/Program.cs#L123)

### Cache

- [MemoryCache](https://github.com/sulmar/dotnet6-minimal-api/blob/master/MinimalApiDemo/Startup/MapEndpoints.cs#L301)

- [Distributed Cache](https://github.com/sulmar/dotnet6-minimal-api/blob/master/MinimalApiDemo/Startup/MapEndpoints.cs#L68)

	- [Redis](https://github.com/sulmar/dotnet6-minimal-api/blob/master/MinimalApiDemo/Program.cs#L78)

### [Autoryzacja](https://minimal-apis.github.io/hello-minimal/#authorization)

### Serializacja

- [Enum](https://github.com/sulmar/dotnet6-minimal-api/blob/master/MinimalApiDemo/Program.cs#L96)

- [NewtonsoftJson](https://github.com/sulmar/vavatech-dotnet-6-microservices-202210/blob/master/src/Shopper/CatalogService.Api/Program.cs#L94)

### [Wersjonowanie](https://github.com/dotnet/aspnet-api-versioning/wiki)

## MVC

### Kontrolery

- Mapowanie tras

	- Przekazywanie parametrów

		- FromRoute
		- FromQuery

			- ComplexType

		- FromHeader
		- FromBody

	- Reguły

		- Wbudowane
		- Własne

### Akcje

- HttpGet
- HttpPost
- HttpPut
- HttpPatch

### Results

- POCO

	- OkResult
	- OkObjectResult
	- NotFoundResult

- ControllerBase

	- Ok
	- NotFound
	- CreatedAtRoute

### OpenAPI

### Obsługa plików

- Pobieranie plików
- Generowanie plików
- Wysyłanie plików

### Walidacja modelu

- DataAnnotations
- FluentValidation

	- ValidationProblemResult

### Wstrzykiwanie zależności

- Rejestracja usłg
- Poprzez konstruktor
- Do metody [FromServices]

### Filtry

- Filtry akcji

### HealthCheck

### Ardalis Endpoints

## Middleware

### Zasada działania Middleware

### Przechwytywanie żądań

- Run
- Use

### [Utworzenie własnej warstwy](https://github.com/sulmar/dotnet6-minimal-api/blob/master/MinimalApiDemo/Middlewares/ApiKeyMiddleware.cs)

## Bezpieczeństwo

### Uwierzytelnianie

- Basic
- JWT Tokens

### Autoryzacja

### Role

### Poświadczenia (Claims)

### Zasady (Policies)



## Dodatki do Visual Studio 
- [Rainbow Braces](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.RainbowBraces)
- [REST Client](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.RestClient)
