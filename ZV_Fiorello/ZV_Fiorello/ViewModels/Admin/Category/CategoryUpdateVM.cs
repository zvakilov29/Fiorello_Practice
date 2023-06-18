using System.ComponentModel.DataAnnotations;

namespace ZV_Fiorello.ViewModels.Admin.Category
{
    public class CategoryUpdateVM
    {
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string? Description { get; set; }
    }
}
