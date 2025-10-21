using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.HomeViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Classes
{
    public class HomeServices : IHomeServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel? GetAnalytics()
        {
            int totalMembers = _unitOfWork.GetRepo<Member>().GetAll().Count();
            int activeMembers = _unitOfWork.GetRepo<MemberPlan>().GetAll(M=>M.Status=="Active").Count();
            int trainers = _unitOfWork.GetRepo<Trainer>().GetAll().Count();
            var allSessions = _unitOfWork.sessionRepo.GetAll();
            int upcomingSessions = allSessions.Where(S=>S.StartDate > DateTime.Now).Count();
            int ongoingSessions = allSessions.Where(S => S.StartDate <= DateTime.Now && S.EndDate > DateTime.Now).Count();
            int completedSessions = allSessions.Where(S => S.EndDate < DateTime.Now).Count();

            return new AnalyticsViewModel()
            {
                TotalMembers = totalMembers,
                ActiveMembers = activeMembers,
                Trainers = trainers,
                UpcomingSessions = upcomingSessions,
                OngoingSessions = ongoingSessions,
                CompletedSessions = completedSessions
            };
        }
    }
}
