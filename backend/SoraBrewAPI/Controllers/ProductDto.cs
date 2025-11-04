using System;
using System.Collections.Generic;

namespace SoraBrewAPI.Models
{
    public class ProductDto
    {
        public string? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }  // ✅ Tambahkan
        public int? Discount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();  // ✅ Tambahkan
        public bool IsPopular { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Computed property - Final price after discount
        public decimal FinalPrice { get; set; }
    }
}