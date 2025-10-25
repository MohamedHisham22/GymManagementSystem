using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberServices _memberServices;

        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }
        public ActionResult Index()
        {
            var data = _memberServices.GetAllMembers();
            return View(data);
        }

        public ActionResult MemberDetails(int id) //this action is the first request
        {
            if (id <= 0) 
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index)); //calling this action is a second request (Index Will Still Have This Error Message)
            }
            var memberDetails = _memberServices.GetMemberDetailsById(id);

            if (memberDetails is null) 
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index)); //calling this action is a second request (Index Will Still Have This Error Message)
            } 

            return View(memberDetails);
        }

        public ActionResult HealthRecordDetails(int id) //this action is the first request
        {
            if (id <= 0) 
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index)); //calling this action is a second request (Index Will Still Have This Error Message)
            } 
            var memberHealthDetails = _memberServices.GetMemberHealthRecordById(id); 
            if (memberHealthDetails is null) 
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index)); //calling this action is a second request (Index Will Still Have This Error Message)
            }
            return View(memberHealthDetails);
        }

        public ActionResult Create() //returns create member view
        {
            return View();
        }

        [HttpPost] // httpGet is the default so we dont have to type it
        public ActionResult CreateMember(CreateMemberViewModel memberForm) //submit member creation
        {
        
            if (!ModelState.IsValid) 
            {
                TempData["ErrorMessage"] = "Please Fill All Required Data Correctly"; 
                return View(nameof(Create) , memberForm); //if user didnt pass view model properties validation reload the same view with his entered data with this error messege
            }
            
            bool isCreated = _memberServices.CreateMember(memberForm);
            if (!isCreated) 
            { 
                TempData["ErrorMessage"] = "Couldnt Create Member"; //if method failed and returned false for any reason
            }
            else 
            {
                TempData["SuccessMessage"] = "Member Created Successfully"; //if method finished and returned true 
            }            
        
        
            return RedirectToAction(nameof(Index)); //second request
        }
        public ActionResult MemberEdit(int id) 
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index)); 
            }
            var membertoUpdate = _memberServices.GetMemberToUpdateById(id);
            if (membertoUpdate is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index)); 
            }
            return View(membertoUpdate); 
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id , MemberToUpdateViewModel memberForm) // the only way the id can get its value is from the route (no query string , no form) //note: both actions need to have the same name so this action can take the id from the route of the other action (if this action had a diffrent name its route would be diffrent so the id would not bind to the other action route id)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please Update Member Data Correctly";
                return View( memberForm); //ruturn view with the action name (same edit view)
            }

            bool isCreated = _memberServices.UpdateMember(id, memberForm);
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
            var memberDetails = _memberServices.GetMemberDetailsById(id);

            if (memberDetails is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index)); 
            }
            ViewBag.MemberId = id; 
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _memberServices.DeleteMember(id);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Couldnt Delete Member";
            }
            else
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
