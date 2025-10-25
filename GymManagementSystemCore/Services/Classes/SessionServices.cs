using AutoMapper;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagementSystemCore.Services.Classes
{
    public class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.sessionRepo.GetAllSessionsWithLoadedCategoriesAndTrainers();
            if (sessions is null || !sessions.Any()) return [];
            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in mappedSessions) 
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.sessionRepo.GetBookedSlotsCount(session.Id);
            }
            return mappedSessions;

        }

        public SessionViewModel? GetSessionDetails(int SessionId)
        {
            var session = _unitOfWork.sessionRepo.GetSessionByIdWithLoadedCategoriesAndTrainers(SessionId);
            if (session is null) return null;
            var mappedSession = _mapper.Map<Session,SessionViewModel>(session);
            mappedSession.AvailableSlots = mappedSession.Capacity - _unitOfWork.sessionRepo.GetBookedSlotsCount(mappedSession.Id);
            return mappedSession;
        }

        public bool CreateSession(CreateSessionViewModel sessionView)
        {
            try 
            {
                if (!IsTrainerExists(sessionView.TrainerID) || !IsCategoryExists(sessionView.CategoryID) || !IsDateTimeValid(sessionView.StartDate,sessionView.EndDate)) return false;
                if(sessionView.Capacity > 25 || sessionView.Capacity < 0) return false;

                var session = _mapper.Map<CreateSessionViewModel,Session>(sessionView);
                _unitOfWork.sessionRepo.Add(session);
                return _unitOfWork.saveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }
        
        public UpdateSessionViewModel? SessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.sessionRepo.GetById(sessionId);
            if (session == null) return null;
            if(!isSessionAvailableToUpdateOrDelete(session)) return null;
            return _mapper.Map<Session, UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel sessionView)
        {
            try 
            {
                var session = _unitOfWork.sessionRepo.GetById(sessionId);
                if (session == null) return false;
                if (!isSessionAvailableToUpdateOrDelete(session) || !IsTrainerExists(sessionView.TrainerID) || !IsDateTimeValid(sessionView.StartDate, sessionView.EndDate)) return false;
                _mapper.Map(sessionView , session);
                _unitOfWork.sessionRepo.Update(session);
                return _unitOfWork.saveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }

        public bool RemoveSession(int sessionId)
        {
            try 
            {
                var session = _unitOfWork.sessionRepo.GetById(sessionId);
                if (session == null) return false;
                if(!isSessionAvailableToUpdateOrDelete(session , true)) return false;
                _unitOfWork.sessionRepo.Delete(session);
                return _unitOfWork.saveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }


        public IEnumerable<SessionCategoriesSelect> GetSessionCategories()
        {
            var categories = _unitOfWork.GetRepo<Category>().GetAll();
            if (categories is null || !categories.Any()) return [];
            var categoriesView = categories.Select(S => new SessionCategoriesSelect()
            {
                id = S.Id,
                Name = S.CategoryName,
            });
            return categoriesView;
        }

        public IEnumerable<SessionTrainersSelect> GetSessionTrainers()
        {
            var trainers = _unitOfWork.GetRepo<Trainer>().GetAll();
            if (trainers is null || !trainers.Any()) return [];
            var trainersView = trainers.Select(S => new SessionTrainersSelect()
            {
                Id = S.Id,
                Name = S.Name,
            });
            return trainersView;
        }


        #region Helpers
        private bool IsTrainerExists(int TrainerId)
        {
            return _unitOfWork.GetRepo<Trainer>().GetById(id: TrainerId) is not null; //return true if not null 
        }


        private bool IsCategoryExists(int CategoryId)
        {
            return _unitOfWork.GetRepo<Category>().GetById(id: CategoryId) is not null; //return true if not null
        }

        private bool IsDateTimeValid(DateTime StartDate, DateTime EndDate)
        {
            return StartDate < EndDate && DateTime.Now < StartDate;
        }

        private bool isSessionAvailableToUpdateOrDelete(Session session , bool deleteSession = false) 
        {
            bool isSessionCompleted = session.EndDate < DateTime.Now;
            bool isSessionStarted = session.StartDate <= DateTime.Now;
            bool isSessionUpcoming = session.StartDate > DateTime.Now;
            bool isSessionStartedAndOngoing = session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now;
            bool doesSessionHaveActiveBooking = _unitOfWork.sessionRepo.GetBookedSlotsCount(session.Id) > 0;
            if (!deleteSession && isSessionCompleted) return false;
            if (!deleteSession && isSessionStarted) return false;
            if (deleteSession && isSessionUpcoming) return false;
            if (deleteSession && isSessionStartedAndOngoing) return false;
            if (doesSessionHaveActiveBooking) return false;
            return true;
        }

        #endregion
    }
}
