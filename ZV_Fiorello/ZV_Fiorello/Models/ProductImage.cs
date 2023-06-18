namespace ZV_Fiorello.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool isMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
