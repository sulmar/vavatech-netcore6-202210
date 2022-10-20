using Vavatech.Shopper.Domain;

namespace Vavatech.Shopper.Api.Maps
{
    // ręcznie / AutoMapper / https://github.com/cezarypiatek/MappingGenerator / Source Generators
    public class Mapper
    {
        public static CustomerDto Map(Customer customer) => new CustomerDto
        {
            Id = customer.Id,
            FullName = $"{customer.FirstName} {customer.LastName}",
            Salary = customer.Salary
        };
    }
}
