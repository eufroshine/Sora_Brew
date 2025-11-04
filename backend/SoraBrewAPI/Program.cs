using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SoraBrewAPI.Models;
using SoraBrewAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<JwtService>();

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Dapatkan IP lokal secara otomatis
string GetLocalIPAddress()
{
    var host = Dns.GetHostEntry(Dns.GetHostName());
    foreach (var ip in host.AddressList)
    {
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
            return ip.ToString();
        }
    }
    return "127.0.0.1";
}

var localIP = GetLocalIPAddress();
Console.WriteLine($"🌐 Local IP Address: {localIP}");

// ✅ Add CORS dengan IP dinamis
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:8080", 
                    "http://localhost:5173",
                    "http://localhost:5245",
                    "https://localhost:5245",
                    $"http://{localIP}:8080",      // ✅ IP lokal port 8080
                    $"http://{localIP}:5173",      // ✅ IP lokal port 5173
                    $"http://{localIP}:5245",      // ✅ IP lokal port 5245
                    $"https://{localIP}:5245"      // ✅ IP lokal port 5245 HTTPS
                  )
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// ✅ Konfigurasi Kestrel untuk listen di semua network interfaces
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5245); // HTTP - accessible dari network
    options.Listen(IPAddress.Any, 7245, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ UseCors harus sebelum UseAuthentication dan UseAuthorization
app.UseCors("AllowVueApp");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Console.WriteLine($"✅ API dapat diakses dari:");
Console.WriteLine($"   - http://localhost:5245");
Console.WriteLine($"   - http://{localIP}:5245");
Console.WriteLine($"   - https://localhost:7245");
Console.WriteLine($"   - https://{localIP}:7245");

app.Run();