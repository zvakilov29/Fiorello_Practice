using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ZV_Fiorello.DAL;
using ZV_Fiorello.Models;
using ZV_Fiorello.ViewModels.Admin.Slider;

namespace ZV_Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Sliders.ToList());
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var slider = _dbContext.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SliderCreateVM data)
        {
            if (data.Photo == null)
            {
                ModelState.AddModelError("Photo", "At least one file should be uploaded!");
                return View(data);
            }

            if (!data.Photo.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Photo", "The file type is incorrect!");
                return View(data);
            }

            if (data.Photo.Length > 1000000)
            {
                ModelState.AddModelError("Photo", "The file size is too large!");
                return View(data);
            }

            string uniqueFileName = Guid.NewGuid() + data.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", uniqueFileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                data.Photo.CopyTo(stream);
            }

            Slider slider = new()
            {
                ImageUrl = uniqueFileName
            };

            _dbContext.Add(slider);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Slider");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var slider = _dbContext.Sliders.Find(id);
            if (slider == null) return NotFound();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.ImageUrl);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }


            _dbContext.Sliders.Remove(slider);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Slider");
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var slider = _dbContext.Sliders.FirstOrDefault(sld => sld.Id == id);
            if (slider == null) return NotFound();
            return View(new SliderUpdateVM());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, SliderUpdateVM data)
        {
            if(id == null) return NotFound();
           

            var slider = _dbContext.Sliders.FirstOrDefault(sld => sld.Id == id);
            if (slider == null) return NotFound();
            if (data.Photo == null)
            {
                ModelState.AddModelError("Photo", "At least one file should be uploaded!");
                return View(data);
            }

            if (!data.Photo.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Photo", "The file type is incorrect!");
                return View(data);
            }

            if (data.Photo.Length > 1000000)
            {
                ModelState.AddModelError("Photo", "The file size is too large!");
                return View(data);
            }


            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", slider.ImageUrl);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string uniqueFileName = Guid.NewGuid() + data.Photo.FileName;
            string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", uniqueFileName);

            using(FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                data.Photo.CopyTo(stream);
            }

            slider.ImageUrl = uniqueFileName;
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Slider");
        }
    }
}
