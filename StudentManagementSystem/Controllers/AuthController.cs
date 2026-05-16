using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAL.Entites;
using StudentManagementSystem.PL.ViewModel.User;

namespace StudentManagementSystem.PL.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            // use selected role
            await _userManager.AddToRoleAsync(user, model.Role);

            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
                return RedirectToAction("Index", "Admin");

            if (roles.Contains("Student"))
                return RedirectToAction("GetAll", "Student");

            if (roles.Contains("Instructor"))
                return RedirectToAction("GetAll", "Instructor");

            return View();
        }
    }
    }
