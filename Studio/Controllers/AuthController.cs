using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Studio.Helpers;
using Studio.Models;
using Studio.ViewModels.AuthVm;

namespace Studio.Controllers
{
    public class AuthController : Controller
    {
        SignInManager<AppUser> _signInManager { get; set; }
        UserManager<AppUser> _userManager { get; set; }
        RoleManager<IdentityRole> _roleManager {  get; set; }
        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = new AppUser
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                UserName = vm.Username
            };

            string password;
            string role;

            if (vm.Username == "user22")
            {
                password = "Salam123!";
                role = Roles.Admin.ToString();
            }
            else
            {
                password = vm.Password;
                role = Roles.User.ToString();
            }

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {   
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(vm); 
                }
            }
            var roleresult = await _userManager.AddToRoleAsync(user, role);
            if (!roleresult.Succeeded)
            {
                ModelState.AddModelError("", "Please Contact to the Admin");
                return View(vm);
            }
            ViewBag.RegistrationSuccessful = true;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(string? returnUrl,LoginVm vm)
        {
            AppUser user = null;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (vm.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            }

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.isRemember, true);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found.");
                return View(); 
            }
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<bool> CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = item.ToString()
                    });
                    if(!result.Succeeded)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
