using Microsoft.Extensions.Diagnostics.HealthChecks;
using Vavatech.Shopper.Api.Services;

namespace Vavatech.Shopper.Api.HealthChecks
{
    public class NbpApiHealthCheck : IHealthCheck
    {
        private readonly NbpApiService nbpApiService;

        public NbpApiHealthCheck(NbpApiService nbpApiService)
        {
            this.nbpApiService = nbpApiService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                await nbpApiService.GetDefaultRate();

                return HealthCheckResult.Healthy();
            }
            catch
            {
                return HealthCheckResult.Degraded();
            }
        }
    }
}
