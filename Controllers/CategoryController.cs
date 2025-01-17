using DeviceManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementApplication.Controllers
{
    public class CategoryController : Controller
    {
        
        private static List<Category> _categories = new List<Category>
        {
            new Category { id = 1, name = "Computer" },
            new Category { id = 2, name = "Printer" },
            new Category { id = 3, name = "Phone" }
        };

        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(_categories);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            _logger.LogInformation("Add action called.");
            if (ModelState.IsValid)
            {
                category.id = _categories.Count + 1;
                _categories.Add(category);
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning("ModelState is not valid.");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var device = _categories.FirstOrDefault(d => d.id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var existedCategory = _categories.FirstOrDefault(d => d.id == category.id);
            if (existedCategory != null && ModelState.IsValid)
            {
                existedCategory.name = category.name;
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(d => d.id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
            return RedirectToAction("Index");
        }
    }
}
