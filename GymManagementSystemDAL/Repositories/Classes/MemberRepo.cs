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
    internal class MemberRepo : IMemberRepo
    {
        //private readonly GymDbContext _dbContext = new GymDbContext(); manual
        private readonly GymDbContext _dbContext; //automatic
        public MemberRepo(GymDbContext dbContext) 
        {
             _dbContext = dbContext;
        }
        public int AddMember(Member member)
        {
            _dbContext.Members.Add(member);
            return _dbContext.SaveChanges(); //saves and returns number of rows affected
        }

        public int DeleteMemberById(int id)
        {
            var member = _dbContext.Members.Find(id);
            if (member == null) 
                return 0;
            _dbContext.Members.Remove(member);  
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Member>? GetAllMembers() => _dbContext.Members.ToList();
       

        public Member? GetMemberById(int id) => _dbContext.Members.Find(id);


        public int UpdateMember(Member member)
        {
           _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
