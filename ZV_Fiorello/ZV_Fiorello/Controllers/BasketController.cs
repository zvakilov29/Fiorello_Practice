using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZV_Fiorello.DAL;
using ZV_Fiorello.Models;
using ZV_Fiorello.ViewModels;

namespace ZV_Fiorello.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BasketController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBasket(int? id)
        {
            //HttpContext.Session.SetString("Book", "Songs of a Dead Dreamer");
            //Response.Cookies.Append("Book", "Songs of a Dead Dreamer");
            if (id == null)
                return NotFound();

            var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            var stringResult = Request.Cookies["basket"];
            List<BasketVM> products;
            if (stringResult == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }

            var existsProduct = products.Find(x => x.Id == product.Id);
            if (existsProduct == null)
            {
                BasketVM basketVM = new BasketVM()
                {
                    Id = product.Id,
                    BasketCount = 1
                };
                products.Add(basketVM);
            }
            else
            {
                existsProduct.BasketCount++;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(15) });

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowBasket()
        {
            //var sessionResult = HttpContext.Session.GetString("Book");
            //var cookiesResult = Request.Cookies["Book"];

            string basket = Request.Cookies["basket"];
            List<BasketVM> products;
            if (basket == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                var basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                products = new List<BasketVM>();

                foreach (var basketProduct in basketProducts)
                {
                    var product = _dbContext.Products
                        .Include(p => p.Images)
                        .SingleOrDefault(p => p.Id == basketProduct.Id);

                    if (product != null)
                    {
                        BasketVM basketVM = new BasketVM()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            ImageUrl = product.Images?.FirstOrDefault(img => img.isMain == true)?.ImageUrl,
                            BasketCount = basketProduct.BasketCount
                        };
                        products.Add(basketVM);
                    }
                }
            }

            return View(products);
        } 

        
    }
}
