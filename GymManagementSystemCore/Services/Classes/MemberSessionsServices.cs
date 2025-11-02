using AutoMapper;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MemberSessionViewModels;
using GymManagementSystemCore.ViewModels.MembershipViewModel;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Classes
{
    public class MemberSessionsServices : IMemberSessionsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberSessionsServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetSessions()
        {
            var sessions = _unitOfWork.sessionRepo.GetAllSessionsWithLoadedCategoriesAndTrainers(S=> S.StartDate > DateTime.Now || (S.StartDate <= DateTime.Now && S.EndDate >= DateTime.Now));
            if (sessions is null || !sessions.Any()) return [];
            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in mappedSessions)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.sessionRepo.GetBookedSlotsCount(session.Id);
            }
            return mappedSessions;
        }
        public IEnumerable<UpcomingSessionViewModel> GetMembersOfUpcomingSession(int sessionId)
        {
            var bookings = _unitOfWork.memberSessionRepo.GetAllBookingsWithMember(B => B.SessionId == sessionId);
            if (bookings is null || !bookings.Any()) return [];
                var memberViews = bookings.Select(B => new UpcomingSessionViewModel()
                {
                    MemberId = B.MemberId,
                    SessionId = B.SessionId,
                    MemberName = B.Member.Name,
                    BookingDate = B.CreatedAt,
                });
                return memberViews;          
        }

        public IEnumerable<OngoingSessionViewModel> GetMembersOfOngoingSession(int sessionId)
        {
            var bookings = _unitOfWork.memberSessionRepo.GetAllBookingsWithMember(B => B.SessionId == sessionId);
            if (bookings is null || !bookings.Any()) return [];
            var memberViews = bookings.Select(B => new OngoingSessionViewModel()
            {
                MemberId = B.MemberId,
                SessionId = B.SessionId,
                MemberName = B.Member.Name,
                isAtended = B.isAttended,
            });
            return memberViews;
        }

        public bool AddMemberToUpcomingSession(CreateMemberInUpcomingViewModel memberView , int sessionId)
        {
            try
            {
                //member exists
                var member = _unitOfWork.GetRepo<Member>().GetById(memberView.MemberId);
                if (member == null) return false;
                //member has active membership
                bool MemberHasMemberShips = _unitOfWork.membershipRepo.GetAll(MS => MS.MemberId == member.Id && MS.Status=="Active").Any();
                if (!MemberHasMemberShips) return false;
                //member cannot book the same session twice (check if this booking exists)
                var booking = _unitOfWork.memberSessionRepo.GetByIdWithComposite(member.Id, sessionId);
                if (booking is not null) return false;
                //session exists
                var session = _unitOfWork.GetRepo<Session>().GetById(sessionId);
                if (session == null) return false;
                //session has available space
                int bookingsCount = _unitOfWork.sessionRepo.GetBookedSlotsCount(session.Id);
                if (session.Capacity == bookingsCount) return false;
                //check if session is upcoming
                bool isUpcoming = session.StartDate > DateTime.Now;
                if (!isUpcoming) return false;

                var memberSession = new MemberSession()
                {
                    MemberId = member.Id,
                    SessionId = session.Id,
                    isAttended = false,
                };
                _unitOfWork.memberSessionRepo.Add(memberSession);
                return _unitOfWork.saveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMemberFromUpcomingSession(int memberId , int sessionId)
        {
            try
            {
                //check if booking exists
                var booking = _unitOfWork.memberSessionRepo.GetByIdWithComposite(memberId, sessionId);
                if (booking is null) return false;
                //check if session is upcoming
                var session = _unitOfWork.sessionRepo.GetById(sessionId);
                if (session.StartDate <= DateTime.Now) return false;
                _unitOfWork.memberSessionRepo.Delete(booking);
                return _unitOfWork.saveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool ToggleAttendanceStatus(int memberId,int sessionId)
        {
            try
            {
                var booking = _unitOfWork.memberSessionRepo.GetByIdWithComposite(memberId, sessionId);
                if (booking == null) return false;
                if (booking.isAttended == true) return false;
                booking.isAttended = true;
                booking.UpdatedAt = DateTime.Now;
                _unitOfWork.memberSessionRepo.Update(booking);
                return _unitOfWork.saveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MembersSelect> GetMmebers()
        {
            var members = _unitOfWork.GetRepo<Member>().GetAll();
            if (members is null || !members.Any()) return [];
            var membersView = members.Select(M => new MembersSelect()
            {
                Id = M.Id,
                Name = M.Name,
            });
            return membersView;
        }
    }
}
