﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _productRepository.Get();

            return Ok(products);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProductById")]
        public ActionResult<Product> Get(int id)
        {
            Product product = _productRepository.Get(id);

            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }

        //[HttpPost]
        //public ActionResult<Product> Post(Product product, [FromServices] IMessageSender messageSender)
        //{
        //    _productRepository.Add(product);
        //    messageSender.Send(product);

        //    return CreatedAtRoute("GetProductById", new { product.Id }, product);
        //}

        [HttpPost]
        public ActionResult<Product> Post(Product product, [FromServices] IMediator mediator)
        {
            mediator.Publish(new AddedProduct(product));

            return CreatedAtRoute("GetProductById", new { product.Id }, product);
        }
        
    }

    public record AddedProduct(Product Product) : INotification;
    
}
