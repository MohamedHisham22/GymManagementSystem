using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class MembershipRepo :  GenericRepo<MemberPlan> , IMembershipRepo
    {
        private readonly GymDbContext _dbContext;

        public MembershipRepo(GymDbContext gymDbContext):base(gymDbContext) 
        {
            _dbContext = gymDbContext;
        }

        public IEnumerable<MemberPlan> GetAllMembershipsWithLoadedMembersAndPlans()
        {
            return _dbContext.MemberShips.AsNoTracking().Include(MS=>MS.Member).Include(MS=>MS.Plan).ToList();
        }


        public MemberPlan? GetMembershipByIdWithLoadedMembersAndPlans(int membershipId)
        {
            return _dbContext.MemberShips.AsNoTracking().Include(MS => MS.Member).Include(MS => MS.Plan).FirstOrDefault(MS=>MS.Id == membershipId);
        }
        public MemberPlan? GetByIdWithComposite(int key1, int key2, DateTime key3) => _dbContext.MemberShips.FirstOrDefault(m =>
        m.MemberId == key1 &&
        m.PlanId == key2 &&
        m.CreatedAt.Date == key3.Date);
    }
}
