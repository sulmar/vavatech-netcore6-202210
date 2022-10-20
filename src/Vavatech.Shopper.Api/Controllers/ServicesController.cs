using Microsoft.AspNetCore.Mvc;

namespace Vavatech.Shopper.Api.Controllers
{
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello services!";
        }

        [HttpGet("{id:int:min(1)}")]
        public ActionResult<string> Get(int id)
        {
            if (id > 10)
                return NotFound();
            else
                return Ok($"Hello service {id}");
        }
    }
}
