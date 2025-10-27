using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IMemberSessionRepo : IGenericRepo<MemberSession>
    {
        public IEnumerable<MemberSession> GetAllBookingsWithMember(Func<MemberSession, bool> predicate);
        public MemberSession? GetByIdWithComposite(int key1, int key2);

    }
}
