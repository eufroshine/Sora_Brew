using MongoDB.Driver;
using Microsoft.Extensions.Options;
using SoraBrewAPI.Models;

namespace SoraBrewAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Product> Products => 
            _database.GetCollection<Product>("products");

        public IMongoCollection<Category> Categories => 
            _database.GetCollection<Category>("categories");

        public IMongoCollection<User> Users => 
            _database.GetCollection<User>("users");

        public IMongoCollection<Order> Orders => 
            _database.GetCollection<Order>("orders");
    }
}