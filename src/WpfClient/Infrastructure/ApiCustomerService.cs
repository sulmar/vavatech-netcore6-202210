using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WpfClient.IServices;
using WpfClient.Models;

namespace WpfClient.Infrastructure
{
    public class WcfCustomerService : ICustomerService
    {
        public Task<IEnumerable<Customer>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class ApiCustomerService : ICustomerService
    {
        private readonly HttpClient client;

        public ApiCustomerService()
            : this(new HttpClient())
        {

        }

        public ApiCustomerService(HttpClient client)
        {           
            this.client = client;

            this.client.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("user-agent", "WPFClient");
            var content = await client.GetStringAsync("/api/customers");

            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(content);

            return customers;
        }
    }
}
