using GymManagementSystemCore.Services.Classes;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MembershipViewModel;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystemPL.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly IMemberShipServices _memberShipServices;

        public MembershipController(IMemberShipServices memberShipServices)
        {
            _memberShipServices = memberShipServices;
        }
        public ActionResult Index()
        {
            var data = _memberShipServices.GetAllMemberships();
            return View(data);
        }

        public ActionResult Create() 
        {
            LoadViewBags();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateMembershipViewModel membershipForm)
        {
            if (!ModelState.IsValid)
            {
                LoadViewBags();
                TempData["Error"] = "Please Fill All Required Data Correctly";
                return View(membershipForm);
            }
            var isCreated = _memberShipServices.CreateMembership(membershipForm);
            if (!isCreated)
            {
                TempData["Error"] = "Membership Failed To Create";
            }
            else
            {
                TempData["Success"] = "Membership Created Successfully";

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Cancel(DateTime startDate, int MemberId , int PlanId) 
        {
            bool isDeleted = _memberShipServices.CancelMembership(startDate,MemberId,PlanId);
            if (!isDeleted)
            {
                TempData["Error"] = "Couldnt Cancel Membership";
            }
            else
            {
                TempData["Success"] = "Membership Canceled Successfully";
            }
            return RedirectToAction(nameof(Index));
        }

        #region Helpers
        private void LoadViewBags()
        {
            var members = _memberShipServices.GetMembershipMembers();
            ViewBag.Members = new SelectList(members, nameof(MembershipMembersSelect.MemberId), nameof(MembershipMembersSelect.MemberName));
            var plans = _memberShipServices.GetMembershipPlans();
            ViewBag.Plans = new SelectList(plans, nameof(MembershipPlansSelect.PlanId), nameof(MembershipPlansSelect.PlanName));
        }
        #endregion
    }
}
