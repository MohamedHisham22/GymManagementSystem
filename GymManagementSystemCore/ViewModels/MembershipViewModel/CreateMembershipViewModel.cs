using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.MembershipViewModel
{
    public class CreateMembershipViewModel
    {
        [Required(ErrorMessage = "Member is required")]
        [Display(Name = "Member")] //asp-for"MemberId" In label Will be only Member Not MemberId
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Plan is required")]
        [Display(Name = "Plan")]
        public int PlanId { get; set; }
    }
}
