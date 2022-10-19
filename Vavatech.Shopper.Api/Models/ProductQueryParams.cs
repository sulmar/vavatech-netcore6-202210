using System.Globalization;

namespace Vavatech.Shopper.Api.Models
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static bool TryParse(string value, out Location result)
        {
            var values = value.Split(':');

            result = new Location(double.Parse(values[0], CultureInfo.InvariantCulture), double.Parse(values[1], CultureInfo.InvariantCulture));

            return true;
        }
    }

    public record LocationRecord(double Latitude, double Longitude)
    {
        public static bool TryParse(string value, out LocationRecord result)
        {
            var values = value.Split(':');

            result = new LocationRecord(double.Parse(values[0], CultureInfo.InvariantCulture), double.Parse(values[1], CultureInfo.InvariantCulture));

            return true;
        }
    }
    

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
                decimal.TryParse(context.Request.Query["to"], out var to) ? to : null
                ));
    }
  
    // Rekord (record)
    public record ProductQueryRecordParams(bool? OnStock, decimal? From, decimal? To)
    {
        public static ValueTask<ProductQueryRecordParams> BindAsync(HttpContext context)
            => new ValueTask<ProductQueryRecordParams>(new ProductQueryRecordParams(
                 bool.TryParse(context.Request.Query["onstock"], out var onstock) ? onstock : null,
                decimal.TryParse(context.Request.Query["from"], out var from) ? from : null,
                decimal.TryParse(context.Request.Query["to"], out var to) ? to : null
                ));

    }
}
