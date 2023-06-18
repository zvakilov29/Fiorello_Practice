using System.ComponentModel;

namespace ZV_Fiorello.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductImage>? Images { get; set; }
    }
}
