using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Models.BaseClasses
{
    public abstract class Base
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } // default time of instance creation
        public DateTime? UpdatedAt { get; set; } 
    }
}
