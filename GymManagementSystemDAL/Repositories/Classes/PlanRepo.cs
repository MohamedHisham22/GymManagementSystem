//using GymManagementSystemDAL.Data.DbContexts;
//using GymManagementSystemDAL.Models;
//using GymManagementSystemDAL.Repositories.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GymManagementSystemDAL.Repositories.Classes
//{
//    public class PlanRepo : IPlanRepo
//    {
//        private readonly GymDbContext _dbContext;
//        public PlanRepo(GymDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }
//        public void AddPlan(Plan plan)
//        {
//            _dbContext.Plans.Add(plan);
//        }

//        public void DeletePlan(Plan plan)
//        {
//            _dbContext.Plans.Remove(plan);
//        }

//        public IEnumerable<Plan>? GetAllPlans() => _dbContext.Plans.ToList();


//        public Plan? GetPlanById(int id) => _dbContext.Plans.Find(id);


//        public void UpdatePlan(Plan plan)
//        {
//            _dbContext.Plans.Update(plan);
//        }
//    }
//}
