using ExamApplication.Enum;
using ExamApplication.Models;
using ExamApplication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExamApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.Email);

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            IdentityResult roleResultInstructor;
            IdentityResult roleResultStudent;

            bool adminRoleExists = await _roleManager.RoleExistsAsync(RoleTypes.Instructor.ToString());
            bool studentRoleExists = await _roleManager.RoleExistsAsync(RoleTypes.Student.ToString());

            if (!adminRoleExists)
            {
                roleResultInstructor = await _roleManager.CreateAsync(new IdentityRole { Name= RoleTypes.Instructor.ToString(), NormalizedName = RoleTypes.Instructor.ToString().ToUpper()});
            }
            if (!studentRoleExists)
            {
                roleResultStudent = await _roleManager.CreateAsync(new IdentityRole { Name = RoleTypes.Student.ToString(), NormalizedName = RoleTypes.Student.ToString().ToUpper() });
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    if (model.Role.Equals(RoleTypes.Student.ToString())){
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Instructor");
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }catch  (Exception ex) {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
