using GymManagementSystemCore.ViewModels.MemberViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Interfaces
{
    public interface IMemberServices
    {
        public IEnumerable<MemberViewModel> GetAllMembers();

        public bool CreateMember(CreateMemberViewModel createdMemberView);

        public MemberDetailsViewModel? GetMemberDetailsById(int id);

        public MemberHealthRecordViewModel? GetMemberHealthRecordById(int id);

        public MemberToUpdateViewModel? GetMemberToUpdateById(int id);
        public bool UpdateMember(int id , MemberToUpdateViewModel memberToUpdateView);

        public bool DeleteMember(int id);

    }
}
