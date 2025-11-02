using GymManagementSystemCore.Services.AuthService;
using GymManagementSystemCore.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Models.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService , SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginForm)
        {
            if (!ModelState.IsValid) return View(loginForm);

            var user = _accountService.ValidateUser(loginForm);
            if(user is null) 
            {
                ModelState.AddModelError("Error", "User Not Found");
                return View(loginForm);
            }
            var result = _signInManager.PasswordSignInAsync(user , loginForm.Password , loginForm.RememberMe , false).Result;
            if (result.Succeeded) return RedirectToAction("Index", "Home");
            if (result.IsNotAllowed)
                ModelState.AddModelError("Error", "User Not Alowed");
            if(result.IsLockedOut)
                ModelState.AddModelError("Error", "User Locked Out");
            return View(loginForm);

        }
        [HttpPost]
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult(); 
            return RedirectToAction("Login");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
