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
    internal class TrainerRepo : ITrainerRepo
    {

        private readonly GymDbContext _dbContext; 
        public TrainerRepo(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddTrainer(Trainer trainer)
        {
            _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        public int DeleteTrainer(Trainer trainer)
        {
            _dbContext.Trainers.Remove(trainer);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Trainer>? GetAllTrainers() => _dbContext.Trainers.ToList();


        public Trainer? GetTrainerById(int id) => _dbContext.Trainers.Find(id);


        public int UpdateTrainer(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
