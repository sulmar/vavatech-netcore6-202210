using FluentValidation;

namespace Vavatech.Shopper.Domain.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.Amount).NotEmpty().InclusiveBetween(1, 10000);
        }
    }
}
