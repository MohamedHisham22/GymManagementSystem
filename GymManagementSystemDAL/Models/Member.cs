using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Member : GymUser
    {
        #region Columns
        public string Photo { get; set; } = null!;
        #endregion

        #region Nav Propeeties (Relations)
        public ICollection<MemberSession>? Bookings { get; set; } = null!;

        public ICollection<MemberPlan>? MemberShips { get; set; } = null!;

        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion
    }
}
