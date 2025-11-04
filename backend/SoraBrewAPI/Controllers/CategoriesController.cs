using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SoraBrewAPI.Models;
using SoraBrewAPI.Services;

namespace SoraBrewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public CategoriesController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var categories = await _mongoDbService.Categories
                .Find(_ => true)
                .SortBy(c => c.Order)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(string id)
        {
            var category = await _mongoDbService.Categories
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            await _mongoDbService.Categories.InsertOneAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
    }
}