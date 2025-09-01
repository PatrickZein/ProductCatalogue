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
            // Data integrity check: Need product name and type
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Type))
                return BadRequest("Product name and type are required.");

            // Data integrity check: assure that the product name isn't already in use
            var existingProduct = await _dbContext.Product
                .FirstOrDefaultAsync(p => p.Name.ToLower() == dto.Name.ToLower());
            if (existingProduct != null)
                return Conflict("A product with the same name already exists.");

            // Data integrity check: assure that the product type exists in the ProductType table
            var productType = await _dbContext.ProductType
                .FirstOrDefaultAsync(pt => pt.Type.ToLower() == dto.Type.ToLower());
            if (productType == null)
                return Conflict("Product type doesn't exist.");

            // Data integrity check: assure that all colours mentioned in the Colours field exist in the Colour table
            var colourNames = dto.Colours.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var colourName in colourNames)
            {
                var colour = await _dbContext.Colour
                    .FirstOrDefaultAsync(c => c.Name.ToLower() == colourName.ToLower());
                if (colour == null)
                    return Conflict($"Colour '{colourName}' doesn't exist.");
            }

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

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            // Return all information about a product by its ID, including type and colours if needed
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
    }
}