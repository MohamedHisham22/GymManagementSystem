using GymManagementSystemCore.ViewModels.MemberSessionViewModels;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Interfaces
{
    public interface IMemberSessionsServices
    {
        public IEnumerable<SessionViewModel> GetSessions();

        public IEnumerable<UpcomingSessionViewModel> GetMembersOfUpcomingSession(int sessionId);

        public IEnumerable<OngoingSessionViewModel> GetMembersOfOngoingSession(int sessionId);


        public bool AddMemberToUpcomingSession(CreateMemberInUpcomingViewModel memberView, int sessionId);

        public bool RemoveMemberFromUpcomingSession(int memberId, int sessionId);
        public bool ToggleAttendanceStatus(int memberId, int sessionId);
        public IEnumerable<MembersSelect> GetMmebers();


    }
}
