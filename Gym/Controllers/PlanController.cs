using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GymManagementSystemPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanServices _planServices;

        public PlanController(IPlanServices planServices)
        {
            _planServices = planServices;
        }
        public ActionResult Index()
        {
            var Data = _planServices.GetAllPlans();
            return View(Data);
        }

        public ActionResult Details(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            }
            var planDetails = _planServices.GetPlanDetails(id);
            if(planDetails is null) 
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(planDetails);
            }
        }

        public ActionResult Edit(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planServices.PlanToUpdate(id);
            if (plan is null) 
            {
                TempData["ErrorMessage"] = "Cannot Edit This Plan Because Its Active Or It Has Active Members";
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(plan);
            }
        }

        [HttpPost]
        public ActionResult Edit([FromRoute]int id , PlanToUpdateViewModel planForm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please Fill All Required Data Correctly";
                return View(planForm);
            }
            var isUpdated = _planServices.UpdatePlanDetails(id, planForm);
            if (!isUpdated) 
            {
                TempData["ErrorMessage"] = "Couldnt Update Plan";
            }
            else 
            {
                TempData["SuccessMessage"] = "Plan Updated Successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Activate(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            }
            bool isSwitched = _planServices.TogglePlanActiveStatus(id);
            
            if (!isSwitched)
            {
                TempData["ErrorMessage"] = "Couldnt Update Plan Status Because It Has Active Members";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["SuccessMessage"] = "Plan Status Updated Successfully";
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
