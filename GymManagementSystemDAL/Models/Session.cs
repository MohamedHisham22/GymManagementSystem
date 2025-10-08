using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Session : Base
    {
        #region Columns
        public string Description { get; set; } = null!;

        public int Capacity { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set;  }

        public int TrainerID { get; set; } //Fk to trainer

        public int CategoryID { get; set; } //Fk to category
        #endregion

        #region Nav Propeeties (Relations)
        public ICollection<MemberSession>? Bookings { get; set; } = null!;

        public Trainer Trainer { get; set; } = null!;

        public Category Category { get; set; } = null!;
        #endregion


    }
}
