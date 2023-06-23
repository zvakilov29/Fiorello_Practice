using ZV_Fiorello.Models;

namespace ZV_Fiorello.Services.ProductServ
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Task<List<Product>> GetAllProductsAsync();

        Product GetProductById(int id);
        Task<Product> GetProductByIdAsync(int id);
    }
}
