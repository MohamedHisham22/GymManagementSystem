using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.MembershipViewModel
{
    public class MembershipViewModel
    {
        public int MemberId { get; set; } 
        public int PlanId { get; set; }
        [Display(Name = "Member")]
        public string MemberName { get; set; } = null!;
        [Display(Name = "Plan")]
        public string PlanName { get; set; } = null!;
        [Display(Name = "Start Date")]
        public DateTime PlanStartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime PlanEndDate { get; set;  }

        public string PlanStartDateDisplay => $"{PlanStartDate: MMM dd , yyyy}";
        public string PlanEndDateDisplay => $"{PlanEndDate: MMM dd , yyyy}";


    }
}
