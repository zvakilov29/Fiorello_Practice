using ZV_Fiorello.Models;

namespace ZV_Fiorello.ViewModels
{
    public class BasketVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int BasketCount { get; set; }
        public string? ImageUrl { get; set; }
    }
}


