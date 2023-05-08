using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger; 
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _productRepository = productRepository; 
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product =await _productRepository.GetProductByIdAsync(id);
            if (product is null)
            {
                return BadRequest();
            }
            return Ok(product);
        }
    }
}