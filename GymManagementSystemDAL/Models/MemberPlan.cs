using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class MemberPlan : Base
    {
        #region Columns

        public DateTime EndDate { get; set; }

        //Computed Property
        public string Status { get 
            {
                if(EndDate <= DateTime.Now) 
                {
                    return "Expired";
                }
                else 
                {
                    return "Active";
                }        
            } 
        }

        public int MemberId { get; set; } //FK to member

        public int PlanId { get; set; } //FK to plan
        #endregion

        #region Nav Properties (Relations)
        public Member Member { get; set; } = null!;

        public Plan Plan { get; set; } = null!;
        #endregion
    }
}
