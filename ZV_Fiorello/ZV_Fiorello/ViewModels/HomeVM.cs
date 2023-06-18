using ZV_Fiorello.Models;

namespace ZV_Fiorello.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; } = new List<Slider>();
        public SliderContent SliderContent { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
