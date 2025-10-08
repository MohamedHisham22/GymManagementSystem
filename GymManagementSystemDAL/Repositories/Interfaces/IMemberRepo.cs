using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    internal interface IMemberRepo
    {
        IEnumerable<Member>? GetAllMembers();

        Member? GetMemberById(int id);

        int AddMember(Member member);

        int UpdateMember(Member member);

        int DeleteMemberById(int id);
    }
}
