using Refit;

namespace Vavatech.Shopper.Api.Services
{    
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly HttpClient client;
        public JsonPlaceholderService(HttpClient client) => this.client = client;
        public async Task<Stream> GetUsers() => await client.GetStreamAsync("/users");
        public async Task<Stream> GetUser(int id) => await client.GetStreamAsync($"/users/{id}");
    }

    // dotnet add package Refit
    public interface IJsonPlaceholderService
    {
        [Get("/users")]
        Task<Stream> GetUsers();
        [Get("/users/{id}")]
        Task<Stream> GetUser(int id);
        
    }
}
