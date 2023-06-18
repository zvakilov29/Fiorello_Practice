using System.ComponentModel.DataAnnotations;

namespace ZV_Fiorello.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "Something should be uploaded you crazy maniac!")]
        public IFormFile? Photo { get; set; }
    }
}
