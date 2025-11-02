using GymManagementSystemDAL.Models.BaseClasses;
using GymManagementSystemDAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class HealthRecord
    {
        #region Columns
        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public BloodType BloodType { get; set; }

        public string? Note { get; set; }

        public int Id {  get; set; } // Shared Pk With Member
        #endregion
    }
}
