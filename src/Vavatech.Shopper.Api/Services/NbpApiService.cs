using Microsoft.Extensions.Options;

namespace Vavatech.Shopper.Api.Services
{
    public class NbpApiServiceOptions
    {        
        public string Table { get; set; }
        public string Code { get; set; }
    }

    public class NbpApiService
    {
        private readonly HttpClient client;

        private readonly NbpApiServiceOptions options;

        public NbpApiService(HttpClient client, IOptions<NbpApiServiceOptions> options)
        {
            this.client = client;
            this.options = options.Value;
        }

        public async Task<Stream> GetRate(string table, string code) => await client.GetStreamAsync($"/api/exchangerates/rates/{table}/{code}/");

        public async Task<Stream> GetDefaultRate() => await GetRate(options.Table, options.Code);
    }
}
