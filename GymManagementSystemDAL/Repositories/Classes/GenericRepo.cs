using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.BaseClasses;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class GenericRepo<T> : IGenericRepo<T> where T : Base, new()
    {
        private readonly GymDbContext _dbContext;
        public GenericRepo(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll(Func<T, bool>? predicate = null)
        {
            if (predicate is null)
               return _dbContext.Set<T>().AsNoTracking().ToList();
            else
               return _dbContext.Set<T>().AsNoTracking().Where(predicate).ToList();
        }

        public T? GetById(int id) => _dbContext.Set<T>().Find(id);


        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
