using GymManagementSystemDAL.Models.BaseClasses;
using GymManagementSystemDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Trainer : GymUser
    {
        #region Columns
        public Specialities Speciality { get; set; }
        #endregion

        #region Nav Properties (Relations)
        public ICollection<Session>? Sessions { get; set; } 
        #endregion
    }
}
