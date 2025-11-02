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
            var result = _signInManager.PasswordSignInAsync(user , loginForm.Password , loginForm.RememberMe , false).Result; //rememberMe is so save his cookie in browser when he closes it if no then the cookie will be saved until the browser is closed then the cookie will be ddeleted , user not authenticated anymore
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
            _signInManager.SignOutAsync().GetAwaiter().GetResult(); //remove cookie from browser , user is not authenticated anymore and will be redirected to login on any request 
            return RedirectToAction("Login");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
