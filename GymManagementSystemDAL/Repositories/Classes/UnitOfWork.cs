using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.BaseClasses;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repos = new();
        private readonly GymDbContext _dbContext;

        public IHealthRecordRepo healthRecordRepo { get; }
        public ISessionRepo sessionRepo { get; }
        public IMembershipRepo membershipRepo { get; }

        public IMemberSessionRepo memberSessionRepo { get; }
        public UnitOfWork(GymDbContext dbContext , IHealthRecordRepo HealthRecordRepo , ISessionRepo SessionRepo , IMembershipRepo MembershipRepo , IMemberSessionRepo MemberSessionRepo)
        {
            _dbContext = dbContext; 
            healthRecordRepo = HealthRecordRepo;
            sessionRepo = SessionRepo;
            membershipRepo = MembershipRepo;
            memberSessionRepo = MemberSessionRepo;
        }

        public IGenericRepo<T> GetRepo<T>() where T : Base, new()
        {
            var entityType = typeof(T);
            if (_repos.TryGetValue(entityType, out var repo))
            {
                return (IGenericRepo<T>)repo;
            }
            var newRepo = new GenericRepo<T>(_dbContext); 
            _repos[entityType] = newRepo;
            return newRepo;
        }

        public int saveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
