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
    internal class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepo<Member>().GetAll();
            if (members == null || !members.Any()) return [];
            var memberViews = members.Select(M => new MemberViewModel
            {
                Id = M.Id,
                Photo = M.Photo,
                Name = M.Name,
                Email = M.Email,
                Gender = M.Gender.ToString(),
                Phone = M.Phone,
            });
            return memberViews;
        }

        public bool CreateMember(CreateMemberViewModel createdMemberView)
        {
            try 
            {     
                #region Check If Phone Or Email Exists If Yes Dont Create User

                if (IsEmailExists(createdMemberView.Email) || IsPhoneExists(createdMemberView.Phone)) return false;
                #endregion

                var member = new Member()
            {
                Name = createdMemberView.Name,
                Email = createdMemberView.Email,
                Phone = createdMemberView.Phone,
                DateOfBirth = createdMemberView.DateOfBirth,
                Gender = createdMemberView.Gender,
                Address = new Address
                {
                    BuildingNumber = createdMemberView.BuildingNumber,
                    Street = createdMemberView.Street,
                    City = createdMemberView.City,
                },
                HealthRecord = new HealthRecord()
                {
                    Height = createdMemberView.HealthRecord.Height,
                    Weight = createdMemberView.HealthRecord.Weight,
                    BloodType = createdMemberView.HealthRecord.BloodType,
                    Note = createdMemberView.HealthRecord.Note,
                }
            };

                _unitOfWork.GetRepo<Member>().Add(member);
                return _unitOfWork.saveChanges() > 0;

            }catch
            {
                return false;
            }
        }

        public MemberDetailsViewModel? GetMemberDetailsById(int id)
        {

            var member = _unitOfWork.GetRepo<Member>().GetById(id);
            if (member == null) return null;
            var memberDetailsView = new MemberDetailsViewModel()
            {
                Id = member.Id,
                Photo = member.Photo,
                Name = member.Name,
                Email = member.Email,
                Gender = member.Gender.ToString(),
                Phone = member.Phone,
                Address = $"{member.Address.BuildingNumber}, {member.Address.Street}, {member.Address.City}",
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
            };
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
            var memberHealthRecordView = new MemberHealthRecordViewModel()
            {
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                BloodType = memberHealthRecord.BloodType,
                Note = memberHealthRecord.Note,
            };
            return memberHealthRecordView;
        }

        public MemberToUpdateViewModel? GetMemberToUpdateById(int id)
        {
            var member =_unitOfWork.GetRepo<Member>().GetById(id);
            if (member == null) return null;
            var memberToUpdateView = new MemberToUpdateViewModel()
            {
                Photo = member.Photo,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City,
            };
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

                member.UpdatedAt = DateTime.Now;
                member.Email = memberToUpdateView.Email;
                member.Phone = memberToUpdateView.Phone;
                member.Address.BuildingNumber = memberToUpdateView.BuildingNumber;
                member.Address.Street = memberToUpdateView.Street;
                member.Address.City = memberToUpdateView.City;

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
                var HasActiveSessions = _unitOfWork.GetRepo<MemberSession>().GetAll(MS => MS.MemberId == id && MS.session.StartDate > DateTime.Now).Any();
                if (HasActiveSessions) return false;
                #endregion

                #region Delete Related MemberShips With Member (not required here in that case because member has cascading relationships)
                var memberShips = memberShipRepo.GetAll(MS => MS.MemberId == id);
                if (memberShips.Any()) 
                {
                    foreach (var memberShip in memberShips)
                    {
                        memberShipRepo.Delete(memberShip);
                    }         
                }
                #endregion
                
                memberRepo.Delete(member);
                return _unitOfWork.saveChanges() > 0;
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
