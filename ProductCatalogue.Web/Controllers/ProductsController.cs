using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogue.Web.DTOs;

namespace ProductCatalogue.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Type))
                return BadRequest("Name and Type are required.");

            var product = new Product
            {
                Name = dto.Name,
                Type = dto.Type,
                Colours = dto.Colours
            };

            _dbContext.Product.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ID }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _dbContext.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var product = await _dbContext.Product.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}