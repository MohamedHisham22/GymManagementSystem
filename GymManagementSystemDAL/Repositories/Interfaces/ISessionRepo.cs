using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    internal interface ISessionRepo
    {
        IEnumerable<Session>? GetAllSessions();

        Session? GetSessionById(int id);

        int AddSession(Session session);

        int UpdateSession(Session session);

        int DeleteSession(Session session);
    }
}
