using GymManagementSystemDAL.Data.DbContexts;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Classes
{
    internal class SessionRepo : ISessionRepo

    {
        private readonly GymDbContext _dbContext;
        public SessionRepo(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddSession(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public int DeleteSession(Session session)
        {
            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session>? GetAllSessions() => _dbContext.Sessions.ToList();


        public Session? GetSessionById(int id) => _dbContext.Sessions.Find(id);


        public int UpdateSession(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
