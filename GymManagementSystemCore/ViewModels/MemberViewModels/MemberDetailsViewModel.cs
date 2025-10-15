using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.ViewModels.MemberViewModels
{
    internal class MemberDetailsViewModel : MemberViewModel
    {
        public string PlanName { get; set; } = null!;

        public string DateOfBirth { get; set; } = null!;

        public string MembershipStartDate { get; set; } = null!;

        public string MembershipEndDate { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
