namespace Vavatech.Shopper.Api.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        private static readonly string[] HeadVerb = new[] { "HEAD" };
        private static readonly string[] PatchVerb = new[] { "PATCH" };

        public static IEndpointConventionBuilder MapHead(this IEndpointRouteBuilder endpoints, 
            string pattern, Delegate requestDelegate)
        {
            return endpoints.MapMethods(pattern, HeadVerb, requestDelegate);
        }

        public static IEndpointConventionBuilder MapPatch(this IEndpointRouteBuilder endpoints,
            string pattern, Delegate requestDelegate)
        {
            return endpoints.MapMethods(pattern, PatchVerb, requestDelegate);
        }
    }
}
