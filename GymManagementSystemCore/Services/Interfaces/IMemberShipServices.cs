using GymManagementSystemCore.ViewModels.MembershipViewModel;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Interfaces
{
    public interface IMemberShipServices
    {
        public IEnumerable<MembershipViewModel> GetAllMemberships();
        public bool CreateMembership(CreateMembershipViewModel membershipView);
        public bool CancelMembership(DateTime key1, int key2, int key3);
        public IEnumerable<MembershipMembersSelect> GetMembershipMembers();
        public IEnumerable<MembershipPlansSelect> GetMembershipPlans();



    }
}
