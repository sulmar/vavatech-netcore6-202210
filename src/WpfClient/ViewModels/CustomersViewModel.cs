using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Infrastructure;
using WpfClient.IServices;
using WpfClient.Models;

namespace WpfClient.ViewModels
{
    internal class CustomersViewModel : INotifyPropertyChanged
    {
        public IEnumerable<Customer> Customers
        {
            get => customers; set
            {
                customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        private readonly ICustomerService customerService;
        private IEnumerable<Customer> customers;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public CustomersViewModel()
            : this(new ApiCustomerService())
        {
        }

        public CustomersViewModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task Load()
        {
            Customers = await customerService.GetAsync();
        }
    }
}
