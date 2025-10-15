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

        //call this for non generic repos
        public IHealthRecordRepo healthRecordRepo { get; } //It Cant Be Called In Generic Repo Bec It Doesnt Implement Base So It Has Its Own Repo
        //public ISessionRepo sessionRepo{ get; }; //It Cant Be Called In Generic Repo Generic Bec It Has More Operations From The Generic Repo


        public UnitOfWork(GymDbContext dbContext , IHealthRecordRepo HealthRecordRepo/* ,ISessionRepo SessionRepo */) 
        {
            _dbContext = dbContext; //DI
            healthRecordRepo = HealthRecordRepo; //DI
            //sessionRepo = SessionRepo //DI
        }


        // call this for generic repos
        public IGenericRepo<T> GetRepo<T>() where T : Base, new()
        {
            var entityType = typeof(T);
            if (_repos.TryGetValue(entityType, out var repo))
            {
                return (IGenericRepo<T>)repo;
            }
            var newRepo = new GenericRepo<T>(_dbContext); //manual object creation (no need for DI)
            _repos[entityType] = newRepo;
            return newRepo;
        }

        //way 2 to handle generic and non generic repos

        #region Temp GetRepo Method
        //public object GetRepo<T>() where T : Base, new()
        //{
        //    var entityType = typeof(T);
        //    if (_repos.TryGetValue(entityType, out var repo)) //return already created object of specific/generic repo
        //    {
        //        return repo;
        //    }

        //    if (entityType == typeof(HealthRecord)) //new specific repo
        //    {
        //        var healthRecordRepo = new HealthRecordRepo(_dbContext);
        //        _repos[entityType] = healthRecordRepo;
        //        return (HealthRecordRepo)healthRecordRepo;
        //    }

        //    if (entityType == typeof(Session)) //new specific repo
        //    {
        //        var sessionRepo = new SessionRepo(_dbContext);
        //        _repos[entityType] = sessionRepo;
        //        return (SessionRepo)sessionRepo;
        //    }

        //    var newRepo = new GenericRepo<T>(_dbContext); //new generic repo
        //    _repos[entityType] = newRepo;
        //    return (IGenericRepo<T>)newRepo;
        //}

        #endregion

        public int saveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
