using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MemberViewModels;
using GymManagementSystemCore.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerServices _trainerServices;

        public TrainerController(ITrainerServices trainerServices)
        {
            _trainerServices = trainerServices;
        }
        public ActionResult Index()
        {
            var data = _trainerServices.GetAllTrainers();
            return View(data);
        }

        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost] 
        public ActionResult CreateTrainer(CreateTrainerViewModel trainerForm) 
        {

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please Fill All Required Data Correctly";
                return View(nameof(Create), trainerForm); 
            }

            bool isCreated = _trainerServices.CreateTrainer(trainerForm);
            if (!isCreated)
            {
                TempData["ErrorMessage"] = "Couldnt Create Trainer"; 
            }
            else
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully"; 
            }


            return RedirectToAction(nameof(Index)); 
        }

        public ActionResult Details(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index)); 
            }
            var memberDetails = _trainerServices.GetTrainerDetails(id);

            if (memberDetails is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index)); 
            }

            return View(memberDetails);
        }
        public ActionResult Edit(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            }
            var membertoUpdate = _trainerServices.GetTrainerToUpdate(id);
            if (membertoUpdate is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(membertoUpdate);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerToUpdateViewModel trainerForm) 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please Update Trainer Data Correctly";
                return View(trainerForm); 
            }

            bool isCreated = _trainerServices.UpdateTrainerDetails(trainerForm, id);
            if (!isCreated)
            {
                TempData["ErrorMessage"] = "Couldnt Update Data";
            }
            else
            {
                TempData["SuccessMessage"] = "Data Updated Successfully";
            }


            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            }
            var memberDetails = _trainerServices.GetTrainerDetails(id);

            if (memberDetails is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _trainerServices.RemoveTrainer(id);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Couldnt Delete Trainer";
            }
            else
            {
                TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
