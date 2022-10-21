using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Vavatech.Shopper.Api.Queries.Payments;

namespace Vavatech.Shopper.Api.Commands.Payments
{
    public class AddPaymentCommand : EndpointBaseAsync.WithRequest<Payment>.WithActionResult<Payment>
    {
        private readonly IPaymentRepository paymentRepository;

        public AddPaymentCommand(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpPost("api/payments")]
        public async override Task<ActionResult<Payment>> HandleAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            paymentRepository.Add(payment);

            return CreatedAtRoute(nameof(GetPaymentByIdQuery), new { Id = payment.Id }, payment);
        }
    }
}
