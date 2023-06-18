using System.ComponentModel.DataAnnotations;
using ZV_Fiorello.Models;

namespace ZV_Fiorello.ViewModels.Admin.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string? Description { get; set; }
    }
}
