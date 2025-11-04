using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SoraBrewAPI.Models;
using SoraBrewAPI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoraBrewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public ProductsController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // ✅ GET all products with optional filters
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll(
            [FromQuery] string? category = null,
            [FromQuery] bool? isPopular = null)
        {
            try
            {
                var filter = Builders<Product>.Filter.Empty;

                if (!string.IsNullOrEmpty(category))
                    filter &= Builders<Product>.Filter.Eq(p => p.Category, category);

                if (isPopular.HasValue)
                    filter &= Builders<Product>.Filter.Eq(p => p.IsPopular, isPopular.Value);

                var products = await _mongoDbService.Products.Find(filter).ToListAsync();

                // 🔥 Convert ke DTO dan hitung FinalPrice
                var productDtos = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    OriginalPrice = p.OriginalPrice,  // ✅ Added
                    Discount = p.Discount,
                    Category = p.Category,
                    Image = p.Image,
                    Tags = p.Tags,  // ✅ Added
                    IsPopular = p.IsPopular,
                    CreatedAt = p.CreatedAt,
                    FinalPrice = p.FinalPrice  // ✅ Use computed property from Product model
                }).ToList();

                return Ok(productDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in GetAll: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, new { 
                    error = "Failed to fetch products", 
                    details = ex.Message 
                });
            }
        }

        // ✅ GET product by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(string id)
        {
            try
            {
                var product = await _mongoDbService.Products
                    .Find(p => p.Id == id)
                    .FirstOrDefaultAsync();

                if (product == null)
                    return NotFound(new { message = "Product not found" });

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    OriginalPrice = product.OriginalPrice,  // ✅ Added
                    Discount = product.Discount,
                    Category = product.Category,
                    Image = product.Image,
                    Tags = product.Tags,  // ✅ Added
                    IsPopular = product.IsPopular,
                    CreatedAt = product.CreatedAt,
                    FinalPrice = product.FinalPrice  // ✅ Use computed property
                };

                return Ok(productDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in GetById: {ex.Message}");
                return StatusCode(500, new { 
                    error = "Failed to fetch product", 
                    details = ex.Message 
                });
            }
        }

        // ✅ CREATE product
        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            try
            {
                product.CreatedAt = DateTime.UtcNow;
                await _mongoDbService.Products.InsertOneAsync(product);
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in Create: {ex.Message}");
                return StatusCode(500, new { 
                    error = "Failed to create product", 
                    details = ex.Message 
                });
            }
        }

        // ✅ UPDATE product (safely)
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Product product)
        {
            try
            {
                product.Id = id;

                var update = Builders<Product>.Update
                    .Set(p => p.Name, product.Name)
                    .Set(p => p.Description, product.Description)
                    .Set(p => p.Price, product.Price)
                    .Set(p => p.OriginalPrice, product.OriginalPrice)  // ✅ Added
                    .Set(p => p.Discount, product.Discount)
                    .Set(p => p.Category, product.Category)
                    .Set(p => p.Image, product.Image)
                    .Set(p => p.Tags, product.Tags)  // ✅ Added
                    .Set(p => p.IsPopular, product.IsPopular)
                    .Set(p => p.CreatedAt, product.CreatedAt);

                var result = await _mongoDbService.Products.UpdateOneAsync(p => p.Id == id, update);

                if (result.MatchedCount == 0)
                    return NotFound(new { message = "Product not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in Update: {ex.Message}");
                return StatusCode(500, new { 
                    error = "Failed to update product", 
                    details = ex.Message 
                });
            }
        }

        // ✅ DELETE product
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var result = await _mongoDbService.Products.DeleteOneAsync(p => p.Id == id);

                if (result.DeletedCount == 0)
                    return NotFound(new { message = "Product not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in Delete: {ex.Message}");
                return StatusCode(500, new { 
                    error = "Failed to delete product", 
                    details = ex.Message 
                });
            }
        }
    }
}