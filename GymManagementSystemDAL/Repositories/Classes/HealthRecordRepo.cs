using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    public class HealthRecordRepo : IHealthRecordRepo
    {
    private readonly GymDbContext _dbContext;
    public HealthRecordRepo(GymDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        public HealthRecord? GetById(int id) => _dbContext.Set<HealthRecord>().Find(id);

    }
}
