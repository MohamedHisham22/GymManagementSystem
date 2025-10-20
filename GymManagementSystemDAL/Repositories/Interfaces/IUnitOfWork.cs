using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.BaseClasses;
using GymManagementSystemDAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ISessionRepo sessionRepo { get; }
        public IHealthRecordRepo healthRecordRepo { get; } 
        public IGenericRepo<T> GetRepo<T>() where T : Base, new();

        public int saveChanges();

    }
}
