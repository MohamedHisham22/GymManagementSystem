using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
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

        public ActionResult MemberDetails(int id) 
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

            return View(memberDetails);
        }

        public ActionResult HealthRecordDetails(int id) 
        {
            if (id <= 0) 
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(Index));
            } 
            var memberHealthDetails = _memberServices.GetMemberHealthRecordById(id); 
            if (memberHealthDetails is null) 
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(memberHealthDetails);
        }

        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost] 
        public ActionResult CreateMember(CreateMemberViewModel memberForm) 
        {
        
            if (!ModelState.IsValid) 
            {
                TempData["ErrorMessage"] = "Please Fill All Required Data Correctly"; 
                return View(nameof(Create) , memberForm); 
            }
            
            bool isCreated = _memberServices.CreateMember(memberForm);
            if (!isCreated) 
            { 
                TempData["ErrorMessage"] = "Couldnt Create Member"; 
            }
            else 
            {
                TempData["SuccessMessage"] = "Member Created Successfully"; 
            }            
        
        
            return RedirectToAction(nameof(Index)); 
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
        public ActionResult MemberEdit([FromRoute]int id , MemberToUpdateViewModel memberForm) 
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please Update Member Data Correctly";
                return View( memberForm); 
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
