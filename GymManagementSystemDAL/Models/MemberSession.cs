using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class MemberSession : Base
    {
        #region Columns
        public bool isAttended { get; set; }
        public int MemberId { get; set; } //Fk to member

        public int SessionId { get; set; } //Fk to session
        #endregion

        #region Nav Properties (Realtions)
        public Member Member { get; set; } = null!;
         
        public Session session { get; set; } = null!;
        #endregion
    }
}
