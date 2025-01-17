using DeviceManagementApplication.Models;
using DeviceManagementApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementApplication.Controllers
{
    public class DeviceController : Controller
    {

        private static List<Category> _categories = new List<Category>
        {
            new Category { id = 1, name = "Computer" },
            new Category { id = 2, name = "Printer" },
            new Category { id = 3, name = "Phone" }
        };
        private static List<Device> _devices = new List<Device>
        {
            new Device { id = 1, name = "Computer 1 cpu i5", code = "eqwcqwrf", categoryId = 1, status = "In Use"},
            new Device { id = 2, name = "Printer 1", code = "fsfwwrr", categoryId = 2, status = "In Use"},
            new Device { id = 3, name = "Phone 1", code = "rwefwefwe", categoryId = 3, status = "In Use"},
            new Device { id = 4, name = "Computer 2 cpu i9", code = "2rewrwer", categoryId = 1, status = "Broken"}
        };
        private readonly ILogger<DeviceController> _logger;
        public DeviceController(ILogger<DeviceController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(int? selectedCategoryId, string searchTerm)
        {
            var filteredDevices = _devices
                .Where(p => (!selectedCategoryId.HasValue || p.categoryId == selectedCategoryId) &&
                    (string.IsNullOrEmpty(searchTerm) || p.name.ToLower().Contains(searchTerm.ToLower())))
                .ToList();

            var model = new DeviceViewModel
            {
                categories = _categories,
                devices = filteredDevices,
                selectedCategoryId = selectedCategoryId,
                searchTerm = searchTerm
            };
            return View(model);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = _categories;
            var model = new Device();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(Device device)
        {
            _logger.LogInformation("Add action called.");
            if (ModelState.IsValid)
            {
                device.id = _devices.Count + 1;
                _devices.Add(device);
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning("ModelState is not valid.");
            }
            ViewBag.Categories = _categories;
            return View(device);
        }

        public IActionResult Edit(int id)
        {
            var device = _devices.FirstOrDefault(d => d.id == id);
            if (device == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _categories;
            return View(device);
        }

        [HttpPost]
        public IActionResult Edit(Device device)
        {
            var existingDevice = _devices.FirstOrDefault(d => d.id == device.id);
            if (existingDevice != null && ModelState.IsValid)
            {
                existingDevice.name = device.name;
                existingDevice.code = device.code;
                existingDevice.categoryId = device.categoryId;
                existingDevice.status = device.status;
                existingDevice.dateOfEntry = device.dateOfEntry;
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _categories;
            return View(device);
        }

        public IActionResult Delete(int id)
        {
            var device = _devices.FirstOrDefault(d => d.id == id);
            if (device != null)
            {
                _devices.Remove(device);
            }
            return RedirectToAction("Index");
        }
    }
}
