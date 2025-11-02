using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.MemberViewModels
{
    public class MemberDetailsViewModel : MemberViewModel
    {
        public string? PlanName { get; set; }

        public string DateOfBirth { get; set; } = null!;

        public string? MembershipStartDate { get; set; }

        public string? MembershipEndDate { get; set; }

        public string Address { get; set; } = null!;
    }
}
