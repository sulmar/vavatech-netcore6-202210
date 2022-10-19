namespace Vavatech.Shopper.Api.Models
{
    public class ProductQueryParams
    {
        public bool? OnStock { get; set; }
        public decimal? From { get; set; }
        public decimal? To { get; set; }

        public ProductQueryParams(bool? onStock, decimal? from, decimal? to)
        {
            OnStock = onStock;
            From = from;
            To = to;
        }

        public static ValueTask<ProductQueryParams> BindAsync(HttpContext context)
            => new ValueTask<ProductQueryParams>(new ProductQueryParams(
                bool.TryParse(context.Request.Query["onstock"], out var onstock) ? onstock : null,
                decimal.TryParse(context.Request.Query["from"], out var from) ? from : null,
                decimal.TryParse(context.Request.Query["to"], out var to) ? to: null));
        

    }
}
