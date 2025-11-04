using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SoraBrewAPI.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("icon")]
        public string Icon { get; set; } = string.Empty;

        [BsonElement("order")]
        public int Order { get; set; }
    }
}