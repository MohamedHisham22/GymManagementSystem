using GymManagementSystemCore.Services.Classes;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MemberSessionViewModels;
using GymManagementSystemCore.ViewModels.MembershipViewModel;
using GymManagementSystemDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystemPL.Controllers
{
    public class SessionScheduleController : Controller
    {
        private readonly IMemberSessionsServices _memberSessionsServices;

        public SessionScheduleController(IMemberSessionsServices memberSessionsServices)
        {
            _memberSessionsServices = memberSessionsServices;
        }
        public ActionResult Index()
        {
            var data = _memberSessionsServices.GetSessions();
            return View(data);
        }
        public ActionResult GetMembersForUpcomingSession(int id) 
        {
            var data = _memberSessionsServices.GetMembersOfUpcomingSession(id);
            ViewBag.SessionId = id;
            return View(data);
        }

        public ActionResult GetMembersForOngoingSession(int id) 
        {
            var data = _memberSessionsServices.GetMembersOfOngoingSession(id);
            ViewBag.SessionId = id;
            return View(data);
        }
        public ActionResult Create() 
        {
            var members = _memberSessionsServices.GetMmebers();
            ViewBag.Members = new SelectList(members, nameof(MembersSelect.Id), nameof(MembersSelect.Name));
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMemberInUpcomingViewModel memberForm , int id) 
        {
            if (!ModelState.IsValid)
            {
                var members = _memberSessionsServices.GetMmebers();
                ViewBag.Members = new SelectList(members, nameof(MembersSelect.Id), nameof(MembersSelect.Name));
                TempData["Error"] = "Please Fill All Required Data Correctly";
                return View(memberForm);
            }
            var isCreated = _memberSessionsServices.AddMemberToUpcomingSession(memberForm,id);
            if (!isCreated)
            {
                TempData["Error"] = "Booking Failed To Create";
            }
            else
            {
                TempData["Success"] = "Booking Created Successfully";

            }
            return RedirectToAction(nameof(GetMembersForUpcomingSession) , new { id });
        }
        [HttpPost]
        public ActionResult Cancel(int MemberId , int SessionId) 
        {
            bool isDeleted = _memberSessionsServices.RemoveMemberFromUpcomingSession(MemberId,SessionId);
            if (!isDeleted)
            {
                TempData["Error"] = "Couldnt Cancel Booking";
            }
            else
            {
                TempData["Success"] = "Booking Canceled Successfully";
            }
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = SessionId });
        }

        public ActionResult AttendMember(int memberId , int sessionId) 
        {
            if (memberId <= 0 || sessionId<=0)
            {
                TempData["ErrorMessage"] = "Id Must Be A Positive Number";
                return RedirectToAction(nameof(GetMembersForOngoingSession), new { id = sessionId });
            }
            bool isSwitched = _memberSessionsServices.ToggleAttendanceStatus(memberId, sessionId);

            if (!isSwitched)
            {
                TempData["ErrorMessage"] = "Couldnt Attend Member";
                return RedirectToAction(nameof(GetMembersForOngoingSession), new { id = sessionId });
            }
            else
            {
                TempData["SuccessMessage"] = "Member Attended Successfully";
                return RedirectToAction(nameof(GetMembersForOngoingSession), new { id = sessionId });
            }
        }
    }
}
