using AutoMapper;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MembershipViewModel;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Classes
{
    public class MemberShipServices : IMemberShipServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberShipServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<MembershipViewModel> GetAllMemberships()
        {
            var memberships = _unitOfWork.membershipRepo.GetAllMembershipsWithLoadedMembersAndPlans();
            if (memberships == null || !memberships.Any()) return [];
            var memberViews = _mapper.Map<IEnumerable<MemberPlan>, IEnumerable<MembershipViewModel>>(memberships);
            return memberViews;

        }
        public bool CreateMembership(CreateMembershipViewModel membershipView)
        {
            try
            {
                //member exists
                var member = _unitOfWork.GetRepo<Member>().GetById(membershipView.MemberId);
                if (member == null) return false;
                //plan exists
                var plan = _unitOfWork.GetRepo<Plan>().GetById(membershipView.PlanId);
                if (plan == null) return false;
                //check if plan is active
                bool isPlanActive = plan.IsActive == true;
                if (!isPlanActive) return false;
                //check if member has active membership (only one active membership for member)
                bool MemberHasMemberShips = _unitOfWork.membershipRepo.GetAll(MS=>MS.MemberId == member.Id).Any();
                if (MemberHasMemberShips) return false;
                var createdMember = _mapper.Map<CreateMembershipViewModel, MemberPlan>(membershipView);
                createdMember.EndDate = DateTime.Now.AddDays(plan.DurationDays);
                _unitOfWork.membershipRepo.Add(createdMember);
                return _unitOfWork.saveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool CancelMembership(DateTime key1 , int key2 , int key3)
        {
            try
            {
                var membership = _unitOfWork.membershipRepo.GetByIdWithComposite(key2,key3,key1);
                if (membership == null) return false;
                //check if membership is active
                if (membership.Status == "Expired") return false;
                _unitOfWork.membershipRepo.Delete(membership);
                return _unitOfWork.saveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MembershipMembersSelect> GetMembershipMembers()
        {
            var members = _unitOfWork.GetRepo<Member>().GetAll();
            if (members is null || !members.Any()) return [];
            var trainersView = members.Select(M => new MembershipMembersSelect()     
            {
                MemberId = M.Id,
                MemberName = M.Name,
            });
            return trainersView;
        }

        public IEnumerable<MembershipPlansSelect> GetMembershipPlans()
        {
            var plans = _unitOfWork.GetRepo<Plan>().GetAll();
            if (plans is null || !plans.Any()) return [];
            var plansView = plans.Select(P => new MembershipPlansSelect()
            {
                PlanId = P.Id,
                PlanName = P.Name,
            });
            return plansView;
        }
    }
}
