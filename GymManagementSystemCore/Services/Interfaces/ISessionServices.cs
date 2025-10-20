using GymManagementSystemCore.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Interfaces
{
    internal interface ISessionServices
    {
        public IEnumerable<SessionViewModel> GetAllSessions();

        public SessionViewModel? GetSessionDetails(int SessionId);

        public bool CreateSession(CreateSessionViewModel session);

        public UpdateSessionViewModel? SessionToUpdate(int sessionId);

        public bool UpdateSession(int sessionId , UpdateSessionViewModel session);

        public bool RemoveSession(int sessionId);


    }
}
