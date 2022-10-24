using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Vavatech.Shopper.Api.AuthorizationRequirements;

namespace Vavatech.Shopper.Api.Controllers
{
    // Kontroler POCO
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;        
        }

        [Authorize(Roles ="Developer,Trainer")]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            if (!this.User.Identity.IsAuthenticated)
                return Unauthorized();

            if (!this.User.IsInRole("Developer"))
                return Forbid();

            string email = this.User.FindFirstValue(ClaimTypes.Email);

            var products = _productRepository.Get();

            return Ok(products);
        }

        // GET api/products/1.json
        // Accept: application/xml
        // [FormatFilter]
        [HttpGet("{id:int:min(1)}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Product>> Get(int id, [FromServices] IAuthorizationService authorizationService)
        {
            Product product = _productRepository.Get(id);

            if (product == null)
                return NotFound();

            var result = await authorizationService.AuthorizeAsync(User, product, new TheSameOwnerRequirement());

            if (result.Succeeded)
                return Ok(product);
            else
                return Forbid();
        }

        //[HttpPost]
        //public ActionResult<Product> Post(Product product, [FromServices] IMessageSender messageSender)
        //{
        //    _productRepository.Add(product);
        //    messageSender.Send(product);

        //    return CreatedAtRoute("GetProductById", new { product.Id }, product);
        //}

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product, [FromServices] IMediator mediator)
        {
            if (product.Color == "Red")
            {
                ModelState.AddModelError("Color", "Czerwony jest zabroniony.");
            }

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            mediator.Publish(new AddedProduct(product));

            return CreatedAtRoute("GetProductById", new { product.Id }, product);
        }
        
    }

    public record AddedProduct(Product Product) : INotification;
    
}
