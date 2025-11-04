using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SoraBrewAPI.Models;
using SoraBrewAPI.Services;

namespace SoraBrewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public OrdersController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            FilterDefinition<Order> filter;

            // Admin can see all orders, customers only see their own
            if (userRole == "admin")
            {
                filter = Builders<Order>.Filter.Empty;
            }
            else
            {
                filter = Builders<Order>.Filter.Eq(o => o.UserId, userId);
            }

            var orders = await _mongoDbService.Orders
                .Find(filter)
                .SortByDescending(o => o.CreatedAt)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetById(string id)
        {
            var order = await _mongoDbService.Orders
                .Find(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            // Check authorization
            if (userRole != "admin" && order.UserId != userId)
                return Forbid();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create(CreateOrderRequest request)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "guest";

            // Calculate totals
            decimal subtotal = request.Items.Sum(item => item.Subtotal);
            decimal tax = subtotal * 0.1m; // 10% tax
            decimal total = subtotal + tax;

            var order = new Order
            {
                UserId = userId,
                OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
                Items = request.Items,
                Subtotal = subtotal,
                Tax = tax,
                Total = total,
                PaymentMethod = request.PaymentMethod,
                DeliveryAddress = request.DeliveryAddress,
                CustomerName = request.CustomerName,
                CustomerPhone = request.CustomerPhone,
                Notes = request.Notes,
                Status = "pending"
            };

            await _mongoDbService.Orders.InsertOneAsync(order);

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateStatus(string id, [FromBody] string status)
        {
            var update = Builders<Order>.Update
                .Set(o => o.Status, status)
                .Set(o => o.UpdatedAt, DateTime.UtcNow);

            var result = await _mongoDbService.Orders
                .UpdateOneAsync(o => o.Id == id, update);

            if (result.MatchedCount == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _mongoDbService.Orders
                .DeleteOneAsync(o => o.Id == id);

            if (result.DeletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }
}