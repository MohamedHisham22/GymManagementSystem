using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IMembershipRepo : IGenericRepo<MemberPlan>
    {
        public IEnumerable<MemberPlan> GetAllMembershipsWithLoadedMembersAndPlans();

        public MemberPlan? GetMembershipByIdWithLoadedMembersAndPlans(int membershipId);

        public MemberPlan? GetByIdWithComposite(int key1 , int key2 , DateTime key3);
    }
}
