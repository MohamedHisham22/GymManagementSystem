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
    public class SessionRepo : GenericRepo<Session>, ISessionRepo

    {
        private readonly GymDbContext _dbContext;
        public SessionRepo(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionsWithLoadedCategoriesAndTrainers(Func<Session, bool>? predicate = null)
        {
            if (predicate==null) 
            {
                return _dbContext.Set<Session>().AsNoTracking()
                                                .Include(S=>S.Trainer)
                                                .Include(S=>S.Category)
                                                .ToList();

            }
            else 
            {
                return _dbContext.Set<Session>().AsNoTracking()
                                                .Include(S => S.Trainer)
                                                .Include(S => S.Category)
                                                .Where(predicate)
                                                .ToList();
            }
        }
        
        
        public Session? GetSessionByIdWithLoadedCategoriesAndTrainers(int sessionId) => _dbContext.Sessions
                                                                                                  .Include(S => S.Category)
                                                                                                  .Include(S => S.Trainer)
                                                                                                  .FirstOrDefault(S=>S.Id == sessionId);
                                                                                                   

        public int GetBookedSlotsCount(int sessionId)
        {
            return _dbContext.Bookings.Count(S => S.SessionId==sessionId);
        }

    }
}
