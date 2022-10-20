using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain.Validators
{
    // https://docs.fluentvalidation.net/
    // dotnet add package FluentValidation
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p =>p.FirstName).NotEmpty().MinimumLength(2).MaximumLength(40);
            RuleFor(p => p.LastName).NotEmpty().MinimumLength(3).MaximumLength(40);
            RuleFor(p => p.Salary).InclusiveBetween(1, 2000);
        }
    }
}
