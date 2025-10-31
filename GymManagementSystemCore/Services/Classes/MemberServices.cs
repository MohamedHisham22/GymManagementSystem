using AutoMapper;
using GymManagementSystemCore.Services.AttachmentService;
using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.MemberViewModels;
using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.BaseClasses;
using GymManagementSystemDAL.Models.Enums;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        public MemberServices(IUnitOfWork unitOfWork , IMapper mapper , IAttachmentService attachmentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepo<Member>().GetAll();
            if (members == null || !members.Any()) return [];
            var memberViews = _mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>> (members);
            return memberViews;
        }

        public bool CreateMember(CreateMemberViewModel createdMemberView)
        {
            try 
            {     
                #region Check If Phone Or Email Exists If Yes Dont Create User

                if (IsEmailExists(createdMemberView.Email) || IsPhoneExists(createdMemberView.Phone)) return false;
                #endregion

                var photoName = _attachmentService.UploadMemberPhoto("memberProfile" , createdMemberView.Photo);
                if(string.IsNullOrEmpty(photoName)) return false;

                var member = _mapper.Map<CreateMemberViewModel, Member>(createdMemberView);
                member.Photo = photoName;
                _unitOfWork.GetRepo<Member>().Add(member);
                bool isCreated = _unitOfWork.saveChanges() > 0;
                if (!isCreated) 
                {
                    _attachmentService.DeleteMemberPhoto("memberProfile", photoName);
                    return false;
                }
                return true;

            }
            catch
            {
                return false;
            }
        }

        public MemberDetailsViewModel? GetMemberDetailsById(int id)
        {

            var member = _unitOfWork.GetRepo<Member>().GetById(id);
            if (member == null) return null;
            var memberDetailsView = _mapper.Map<Member, MemberDetailsViewModel>(member);

            //get active memberships
            var activeMemberShip = _unitOfWork.GetRepo<MemberPlan>().GetAll(MS => MS.MemberId == id && MS.Status == "Active").FirstOrDefault();

            if(activeMemberShip is not null) 
            {
                var plan = _unitOfWork.GetRepo<Plan>().GetById(activeMemberShip.PlanId);
                memberDetailsView.PlanName = plan!.Name;
                memberDetailsView.MembershipEndDate = activeMemberShip.EndDate.ToShortDateString();
                memberDetailsView.MembershipStartDate = activeMemberShip.CreatedAt.ToShortDateString();
            }

            return memberDetailsView;
        }

        public MemberHealthRecordViewModel? GetMemberHealthRecordById(int id)
        {
            var memberHealthRecord = _unitOfWork.healthRecordRepo.GetById(id);
            if (memberHealthRecord == null) return null;
            var memberHealthRecordView = _mapper.Map<HealthRecord, MemberHealthRecordViewModel>(memberHealthRecord);
            return memberHealthRecordView;
        }

        public MemberToUpdateViewModel? GetMemberToUpdateById(int id)
        {
            var member =_unitOfWork.GetRepo<Member>().GetById(id);
            if (member == null) return null;
            var memberToUpdateView = _mapper.Map<Member,MemberToUpdateViewModel>(member);
            return memberToUpdateView;
        }

        public bool UpdateMember(int id, MemberToUpdateViewModel memberToUpdateView)
        {
            try
            {
                var memberRepo = _unitOfWork.GetRepo<Member>();
                var member = memberRepo.GetById(id);
                if (member == null) return false;

                #region Check If Phone Or Email Exists If Yes Dont Create User
                if ((member.Email != memberToUpdateView.Email && IsEmailExists(memberToUpdateView.Email)) 
                || ( member.Phone != memberToUpdateView.Phone && IsPhoneExists(memberToUpdateView.Phone))) return false;
                #endregion

                _mapper.Map(memberToUpdateView , member);
                memberRepo.Update(member);
                return _unitOfWork.saveChanges() > 0;

            }
            catch
            {
                return false;
            }
        }
        
        public bool DeleteMember(int id)
        {
            try
            {
                var memberRepo = _unitOfWork.GetRepo<Member>();
                var memberShipRepo = _unitOfWork.GetRepo<MemberPlan>();
                var member = memberRepo.GetById(id);
                if (member == null) return false;

                #region Check If Member Has Active Membership Dont Delete
                var sessionIds = _unitOfWork.GetRepo<MemberSession>().GetAll(MS=>MS.MemberId == id).Select(s => s.SessionId);
                var HasActiveSessions = _unitOfWork.sessionRepo.GetAll(S => sessionIds.Contains(S.Id) && S.StartDate > DateTime.Now).Any();
                if (HasActiveSessions) return false;
                #endregion

                memberRepo.Delete(member);
                bool isDeleted =  _unitOfWork.saveChanges() > 0;
                if(!isDeleted) return false;
                bool isPhotoDeleted = _attachmentService.DeleteMemberPhoto("memberProfile", member.Photo);
                if (!isPhotoDeleted) return false;
                return true;
                
            }
            catch 
            {
                return false;
            }
        }

        #region Helpers
        public bool IsEmailExists(string email) => _unitOfWork.GetRepo<Member>().GetAll(M => M.Email == email).Any();
        public bool IsPhoneExists(string phone) => _unitOfWork.GetRepo<Member>().GetAll(M => M.Phone == phone).Any();
        #endregion
    }
    
}
