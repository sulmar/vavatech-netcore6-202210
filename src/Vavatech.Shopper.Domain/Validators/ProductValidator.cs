using FluentValidation;

namespace Vavatech.Shopper.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Color).NotEmpty();
            RuleFor(p => p.Price).InclusiveBetween(1, 1000);
        }
    }
}
