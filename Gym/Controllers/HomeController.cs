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
        public IActionResult Index()
        {
            var data = _homeServices.GetAnalytics(); //now its view model is loaded 
            //return View(); //Return Default View (View With Action Name [Index])
            return View(data); //Return Default View (View With Action Name [Index]) With Passing Model Data To That View
            //return View(viewName) //Return View With That Name Instead Of View With Action Name
            //return View(viewName,model) //Return View With That Name Instead Of View With Action Name With Passing Model Data To That View

            //now view model is loaded and its data is passed to the view the view only needs to define the model that it will use with @model modelName then use @Model to instance it and use its fields
        }
    }
}
