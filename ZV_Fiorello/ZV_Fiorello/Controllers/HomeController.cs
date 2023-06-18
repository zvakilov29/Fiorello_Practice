using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ZV_Fiorello.DAL;
using ZV_Fiorello.ViewModels;

namespace ZV_Fiorello.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new();
            homeVM.Sliders = _appDbContext.Sliders.ToList();
            homeVM.SliderContent = _appDbContext.SliderContents.FirstOrDefault();
            homeVM.Categories = _appDbContext.Categories.ToList();
            homeVM.Products = _appDbContext.Products.Include(p=>p.Images).ToList();
            return View(homeVM);
        }



    }
}