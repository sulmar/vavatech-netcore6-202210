namespace Vavatech.Shopper.Api.Services
{
    public class NbpApiService
    {
        private readonly HttpClient client;

        public NbpApiService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Stream> GetRate(string table, string code) => await client.GetStreamAsync($"/api/exchangerates/rates/{table}/{code}/");
    }
}
