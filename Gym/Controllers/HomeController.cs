using GymManagementSystemCore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    //[AllowAnonymous] //default ==> anyone can access this page without being authenticated
    [Authorize] //any authenticated user can access this page
    //[Authorize(Roles="SuperAdmin")] //user must be authenticated and has SupeAdmin role to access this page or else he will be directed to access denied action that is specified in the AccessDeniedPath in the ConfigureApplicationCookie
    public class HomeController : Controller
    {
        private readonly IHomeServices _homeServices;

        public HomeController(IHomeServices homeServices)
        {
            _homeServices = homeServices;
        }
        public ActionResult Index()
        {
            var data = _homeServices.GetAnalytics(); 
            return View(data); 
        }
    }
}
