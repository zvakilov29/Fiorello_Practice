using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZV_Fiorello.DAL;

namespace ZV_Fiorello.ViewComponents.ProductVC
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public ProductViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take, int skip)
        {
            var products = await _dbContext.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return View(products);
        }
    }
}
