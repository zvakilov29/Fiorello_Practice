using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZV_Fiorello.DAL;
using ZV_Fiorello.ViewModels;

namespace ZV_Fiorello.ViewComponents.HeaderVC
{
    public class CartViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartViewComponent(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int cartItemCount = await GetCartItemCountFromCookiesAsync();

            return View("~/Views/Shared/Components/Header/Cart.cshtml", cartItemCount);
        }

        private async Task<int> GetCartItemCountFromCookiesAsync()
        {
            var basketCookie = _httpContextAccessor.HttpContext.Request.Cookies["basket"];
            List<BasketVM> basket;
            if (basketCookie == null)
            {
                basket = new List<BasketVM>();
            }
            else
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(basketCookie);
            }

            var totalItemCount = 0;
            foreach (var item in basket)
            {
                totalItemCount += item.BasketCount;
            }

            return totalItemCount;
        }
    }
}
