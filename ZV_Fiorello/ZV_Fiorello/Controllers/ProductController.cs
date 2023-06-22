using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZV_Fiorello.DAL;
using ZV_Fiorello.ViewModels;

namespace ZV_Fiorello.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var products = _appDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Take(3)
                .ToList();
            ViewBag.ProductsCount = _appDbContext.Products.Count();
            return View(products);
        }

        public IActionResult LoadMore(int skip)
        {
            var products = _appDbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Skip(skip)
            .Take(3)
            .ToList();


            return PartialView("_LoadMorePartial", products);
        }

        public IActionResult Search(string searchText)
        {
            var searchedProducts = _appDbContext.Products
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()))
                .ToList();

            return PartialView("_SearchPartial", searchedProducts);
        }
    }
}
