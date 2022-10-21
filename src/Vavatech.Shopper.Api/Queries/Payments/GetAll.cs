using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Vavatech.Shopper.Api.Queries.Payments
{

    // dotnet add package Ardalis.ApiEndpoints

    // GET api/payments
    public class GetAll : EndpointBaseAsync.WithoutRequest.WithResult<IEnumerable<Payment>>
    {
        private readonly IPaymentRepository paymentRepository;

        public GetAll(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpGet("/api/payments")]
        public override Task<IEnumerable<Payment>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var payments = paymentRepository.Get();

            return Task.FromResult(payments);
        }
    }
}
