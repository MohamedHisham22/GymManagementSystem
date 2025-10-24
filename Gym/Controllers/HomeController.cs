using GymManagementSystemCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
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
