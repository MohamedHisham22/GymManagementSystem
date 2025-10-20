using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface ISessionRepo : IGenericRepo<Session>
    {
        public IEnumerable<Session> GetAllSessionsWithLoadedCategoriesAndTrainers();

        public Session? GetSessionByIdWithLoadedCategoriesAndTrainers(int sessionId);

        public int GetBookedSlotsCount(int SessionId);
    }
}
