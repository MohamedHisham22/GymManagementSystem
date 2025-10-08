using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    internal class PlanRepo : IPlanRepo
    {
        private readonly GymDbContext _dbContext;
        public PlanRepo(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddPlan(Plan plan)
        {
            _dbContext.Plans.Add(plan);
            return _dbContext.SaveChanges();
        }

        public int DeletePlan(Plan plan)
        {
            _dbContext.Plans.Remove(plan);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Plan>? GetAllPlans() => _dbContext.Plans.ToList();


        public Plan? GetPlanById(int id) => _dbContext.Plans.Find(id);


        public int UpdatePlan(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
