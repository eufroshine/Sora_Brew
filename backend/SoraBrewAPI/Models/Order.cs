using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SoraBrewAPI.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("orderNumber")]
        public string OrderNumber { get; set; } = string.Empty;

        [BsonElement("items")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [BsonElement("subtotal")]
        public decimal Subtotal { get; set; }

        [BsonElement("tax")]
        public decimal Tax { get; set; }

        [BsonElement("total")]
        public decimal Total { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = "pending"; // pending, processing, completed, cancelled

        [BsonElement("paymentMethod")]
        public string PaymentMethod { get; set; } = string.Empty;

        [BsonElement("deliveryAddress")]
        public string DeliveryAddress { get; set; } = string.Empty;

        [BsonElement("customerName")]
        public string CustomerName { get; set; } = string.Empty;

        [BsonElement("customerPhone")]
        public string CustomerPhone { get; set; } = string.Empty;

        [BsonElement("notes")]
        public string? Notes { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class OrderItem
    {
        [BsonElement("productId")]
        public string ProductId { get; set; } = string.Empty;

        [BsonElement("productName")]
        public string ProductName { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("subtotal")]
        public decimal Subtotal { get; set; }
    }

    public class CreateOrderRequest
    {
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public string PaymentMethod { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}