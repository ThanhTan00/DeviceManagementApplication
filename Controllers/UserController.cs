using DeviceManagementApplication.Models.ViewModels;
using DeviceManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagementApplication.Controllers
{
    public class UserController : Controller
    {
        private static List<User> _user = new List<User>
        {
            new User { id = 1, fullName = "Le Nguyen Thanh Tan", email = "tan.lenguyen.cit19@eiu.edu.vn", phoneNumber =  "0768613782" },
            new User { id = 1, fullName = "Nguyen Truong Giang", email = "giang.nguyentruong.cit19@eiu.edu.vn", phoneNumber =  "0128564964" },
            new User { id = 1, fullName = "Le Nguyen Thanh Tan", email = "dat.nguyenthanh.cit19@eiu.edu.vn", phoneNumber =  "0988475452" },

        };
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_user);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(User user)
        {
            _logger.LogInformation("Add action called.");
            if (ModelState.IsValid)
            {
                user.id = _user.Count + 1;
                _user.Add(user);
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning("ModelState is not valid.");
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _user.FirstOrDefault(d => d.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            var existedUser = _user.FirstOrDefault(d => d.id == user.id);
            if (existedUser != null && ModelState.IsValid)
            {
                existedUser.fullName = user.fullName;
                existedUser.phoneNumber = user.phoneNumber;
                existedUser.email = user.email;
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _user.FirstOrDefault(d => d.id == id);
            if (user != null)
            {
                _user.Remove(user);
            }
            return RedirectToAction("Index");
        }

    }
}

