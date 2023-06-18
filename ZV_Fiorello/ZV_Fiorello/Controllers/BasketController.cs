using Microsoft.AspNetCore.Mvc;
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

            
            var product = _dbContext.Products.Find(id);
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
            if(existsProduct == null)
            {
                BasketVM basketVM = new BasketVM()
                {
                    Id = product.Id,
                    Name = product.Name,
                    BasketCount = 1
                };
                products.Add(basketVM);
            }
            else
            {
                existsProduct.BasketCount++;
            }
           


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new CookieOptions { MaxAge = TimeSpan.FromMinutes(15) });

            return Content($"{product.Name} submitted successfully");
        }

        public IActionResult ShowBasket()
        {
            //var sessionResult = HttpContext.Session.GetString("Book");
            //var cookiesResult = Request.Cookies["Book"];


            var result = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            return Json(result);
        } 

        
    }
}
