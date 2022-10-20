using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Models;

namespace WpfClient.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAsync();
    }
}
