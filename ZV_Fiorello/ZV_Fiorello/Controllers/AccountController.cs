using Microsoft.AspNetCore.Mvc;

namespace ZV_Fiorello.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name)
        {
            return View();
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string name)
        {
            return View();
        }
    }
}
