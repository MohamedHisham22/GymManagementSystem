using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Plan : Base
    {
        #region Columns
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
        #endregion

        #region Nav Properties (Relations)
        public ICollection<MemberPlan>? MemberShips { get; set; } = null!;
        #endregion
    }
}
