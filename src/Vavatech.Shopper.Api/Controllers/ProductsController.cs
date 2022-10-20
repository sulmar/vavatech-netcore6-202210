using Microsoft.AspNetCore.Mvc;

namespace Vavatech.Shopper.Api.Controllers
{
    // Kontroler POCO

    [Route("api/items")]
    public class ProductsController 
    {
        [HttpGet]
        public string Get()
        {
            return "Hello Items!";
        }

        [HttpGet("{id:int:min(1)}")]
        [FormatFilter]
        public ActionResult<string> Get(int id)
        {
            if (id > 10)
                return new NotFoundResult();
            else
                return new OkObjectResult($"Hello item {id}");
        }
    }
}
