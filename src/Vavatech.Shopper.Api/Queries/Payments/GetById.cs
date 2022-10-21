using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Vavatech.Shopper.Api.Queries.Payments
{
    public class GetPaymentByIdQuery : EndpointBaseAsync.WithRequest<int>.WithResult<Payment>
    {
        private readonly IPaymentRepository paymentRepository;

        public GetPaymentByIdQuery(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpGet("/api/payments/{id}", Name = nameof(GetPaymentByIdQuery))]
        public override Task<Payment> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var payment = paymentRepository.Get(id);

            return Task.FromResult(payment);
        }
    }
}
