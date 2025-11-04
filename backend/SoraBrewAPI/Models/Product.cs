using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SoraBrewAPI.Models
{
    [BsonIgnoreExtraElements]  // ✅ Tambahkan ini untuk ignore field yang tidak dikenali
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("originalPrice")]  // ✅ Tambahkan ini
        public decimal? OriginalPrice { get; set; }

        [BsonElement("discount")]
        public int? Discount { get; set; } = 0;

        [BsonElement("category")]
        public string Category { get; set; } = string.Empty;

        [BsonElement("image")]
        public string Image { get; set; } = string.Empty;

        [BsonElement("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [BsonElement("isPopular")]
        public bool IsPopular { get; set; } = false;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Properti ini tidak disimpan di MongoDB, hanya dikirim saat response JSON
        [BsonIgnore]
        public decimal FinalPrice
        {
            get
            {
                if (Discount.HasValue && Discount.Value > 0)
                {
                    return Price - (Price * Discount.Value / 100);
                }
                return Price;
            }
        }
    }
}