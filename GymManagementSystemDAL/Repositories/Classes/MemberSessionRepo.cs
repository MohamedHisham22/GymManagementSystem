using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class MemberSessionRepo : GenericRepo<MemberSession>, IMemberSessionRepo
    {
        private readonly GymDbContext _dbContext;

        public MemberSessionRepo(GymDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<MemberSession> GetAllBookingsWithMember(Func<MemberSession,bool> predicate)
        {
            var bookings = _dbContext.Bookings.AsNoTracking().Include(B=>B.Member).Where(predicate).ToList();
            return bookings;
        }

        public MemberSession? GetByIdWithComposite(int key1, int key2) => _dbContext.Bookings.Find(key1,key2);

    }
}
