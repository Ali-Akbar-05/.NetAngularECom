

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly StoreContext _dbCon;

        public ProductController(ILogger<ProductController> logger, StoreContext dbCon)
        {
            _dbCon = dbCon;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _dbCon.Products.ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product =await _dbCon.Products.Where(b => b.Id == id).FirstOrDefaultAsync();
            if (product is null)
            {
                return BadRequest();
            }
            return Ok(product);
        }
    }
}