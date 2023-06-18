using Microsoft.AspNetCore.Mvc;
using ZV_Fiorello.DAL;
using ZV_Fiorello.Models;
using ZV_Fiorello.ViewModels.Admin.Category;

namespace ZV_Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Categories.ToList());
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if(category == null) return NotFound();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CategoryCreateVM categoryVM)
        {
            if (!ModelState.IsValid) return View(categoryVM);
            var categoryExists = _dbContext.Categories.Any(cat => cat.Name.ToLower() == categoryVM.Name.ToLower());
            if (categoryExists)
            {
                ModelState.AddModelError("Name", "category with the given name already exists!");
                return View(categoryVM);
            }
            Category newCategory = new()
            {
                Name = categoryVM.Name,
                Description = categoryVM.Description
            };
            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();


            return RedirectToAction("Index", "Category");
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(new CategoryUpdateVM { Name = category.Name, Description = category.Description});
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, CategoryUpdateVM categoryVM)
        {
            if (!ModelState.IsValid) return View(categoryVM);
            var duplicatedCategory = _dbContext.Categories.Any(cat => cat.Name.ToLower() == categoryVM.Name.ToLower() && cat.Id != id);
            if (duplicatedCategory)
            {
                ModelState.AddModelError("Name", "category with the given name already exists!");
                return View(categoryVM);
            }
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            category.Name = categoryVM.Name;
            category.Description = categoryVM.Description;
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = _dbContext.Categories.Find(id);
            if(category == null) return NotFound();
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

    }
}
