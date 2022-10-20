namespace Vavatech.Shopper.Api.Startup
{
    public static class MapEndpointsExtensions
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            app.MapBasicEndpoints();
            app.MapCustomersEndpoints();
            app.MapExchangeEndpoints();
            app.MapReportsEndpoints();
            app.MapUsersEndpoints();

            return app;
        }
    }
}
