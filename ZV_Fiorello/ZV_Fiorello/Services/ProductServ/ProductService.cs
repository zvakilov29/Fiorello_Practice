using Microsoft.EntityFrameworkCore;
using ZV_Fiorello.DAL;
using ZV_Fiorello.Models;

namespace ZV_Fiorello.Services.ProductServ
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService (AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAllProducts()
        {
            return _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .ToList();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .ToListAsync();
        }
    }
}
