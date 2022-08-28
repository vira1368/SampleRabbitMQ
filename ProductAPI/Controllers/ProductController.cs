using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.RabbitMQ;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProducer _producer;

        public ProductController(IProductService productService, IProducer producer)
        {
            _productService = productService;
            _producer = producer;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost]
        public Product AddProduct(Product product)
        {
            var productData = _productService.AddProduct(product);
            _producer.SendMessage(productData);
            return productData;
        }

        [HttpPut]
        public Product UpdateProduct(Product product)
        {
            return _productService.UpdateProduct(product);
        }

        [HttpDelete]
        public bool DeleteProduct(int id)
        {
            return _productService.DeleteProduct(id);
        }
    }
}
