using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SoraBrewAPI.Models;
using SoraBrewAPI.Services;

namespace SoraBrewAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;
        private readonly JwtService _jwtService;

        public AuthController(MongoDbService mongoDbService, JwtService jwtService)
        {
            _mongoDbService = mongoDbService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            // Check if user already exists
            var existingUser = await _mongoDbService.Users
                .Find(u => u.Email == request.Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
                return BadRequest(new { message = "Email already registered" });

            // Create new user
            var user = new User
            {
                Email = request.Email,
                Password = _jwtService.HashPassword(request.Password),
                FullName = request.FullName,
                Phone = request.Phone,
                Role = "customer"
            };

            await _mongoDbService.Users.InsertOneAsync(user);

            // Generate token
            var token = _jwtService.GenerateToken(user);

            // Remove password from response
            user.Password = string.Empty;

            return Ok(new AuthResponse
            {
                Token = token,
                User = user
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            // Find user
            var user = await _mongoDbService.Users
                .Find(u => u.Email == request.Email && u.IsActive)
                .FirstOrDefaultAsync();

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            // Verify password
            if (!_jwtService.VerifyPassword(request.Password, user.Password))
                return Unauthorized(new { message = "Invalid email or password" });

            // Generate token
            var token = _jwtService.GenerateToken(user);

            // Remove password from response
            user.Password = string.Empty;

            return Ok(new AuthResponse
            {
                Token = token,
                User = user
            });
        }

        [HttpGet("me")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _mongoDbService.Users
                .Find(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            user.Password = string.Empty;
            return Ok(user);
        }
    }
}