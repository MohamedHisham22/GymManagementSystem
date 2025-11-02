using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models
{
    public class Category : Base
    {
        #region Columns
        public string CategoryName { get; set; } = null!;

        #endregion

        #region Nav Properties (Relations)
        public ICollection<Session>? Sessions { get; set; }

        #endregion
    }
}
