using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystemPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionServices _sessionServices;

        public SessionController(ISessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }
        public ActionResult Index()
        {
            var data = _sessionServices.GetAllSessions();
            return View(data);
        }

        public ActionResult Details(int id) 
        {
            if (id <= 0) 
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            } 
            var sessionDetails = _sessionServices.GetSessionDetails(id);
            if (sessionDetails == null) 
            {
                TempData["ErrorMessage"] = "Couldnt Find Session";
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(sessionDetails);
            }
        }

        public ActionResult Create() 
        {
            LoadViewBags(true);
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel sessionForm)
        {
            if (!ModelState.IsValid)
            {
                LoadViewBags(true);
                TempData["ErrorMessage"] = "Please Fill All Required Data Correctly";
                return View(sessionForm);
            }
            var isCreated = _sessionServices.CreateSession(sessionForm);
            if (!isCreated) 
            {
                TempData["ErrorMessage"] = "Session Failed To Create";
            }
            else 
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            }
            var sessionDetails = _sessionServices.SessionToUpdate(id);
            if (sessionDetails == null)
            {
                TempData["ErrorMessage"] = "Cant Edit Ongoing And Completed Sessions And Sessions That Has Bookings";
                return RedirectToAction(nameof(Index));
            }
            LoadViewBags();
            return View(sessionDetails);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel sessionForm) 
        {
            if (!ModelState.IsValid)
            {
                LoadViewBags();
                TempData["ErrorMessage"] = "Please Fill All Required Data Correctly";

                return View(sessionForm);
            }
            var isUpdated = _sessionServices.UpdateSession(id, sessionForm);
            if (!isUpdated)
            {
                TempData["ErrorMessage"] = "Session Failed To Update";
            }
            else
            {
                TempData["SuccessMessage"] = "Session Updated Successfully";

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
            var session = _sessionServices.GetSessionDetails(id);
            if (session == null) 
            {
                TempData["ErrorMessage"] = "Couldnt Find Session";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SessionId = id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id) 
        {
            bool isDeleted = _sessionServices.RemoveSession(id);
            if (!isDeleted) 
            {
                TempData["ErrorMessage"] = "Cannot Delete Ongoing And Upcoming Sessions";
            }
            else 
            {
                TempData["SuccessMessage"] = "Session Deleted Successfully";
            }
            return RedirectToAction(nameof(Index));
        }
        #region Helpers
        private void LoadViewBags(bool BothBags = false) 
        {
            if (BothBags == true) 
            {
                var categories = _sessionServices.GetSessionCategories();
                ViewBag.Categories = new SelectList(categories, nameof(SessionCategoriesSelect.id), nameof(SessionCategoriesSelect.Name));
            }
            var trainers = _sessionServices.GetSessionTrainers();
            ViewBag.Trainers = new SelectList(trainers, nameof(SessionTrainersSelect.Id), nameof(SessionTrainersSelect.Name));
        }
        #endregion
    }
}
