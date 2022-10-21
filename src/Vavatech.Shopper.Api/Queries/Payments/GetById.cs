using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Vavatech.Shopper.Api.Queries.Payments
{
    public class GetById : EndpointBaseAsync.WithRequest<int>.WithResult<Payment>
    {
        private readonly IPaymentRepository paymentRepository;

        public GetById(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpGet("/api/payments/{id}")]
        public override Task<Payment> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var payment = paymentRepository.Get(id);

            return Task.FromResult(payment);
        }
    }
}
