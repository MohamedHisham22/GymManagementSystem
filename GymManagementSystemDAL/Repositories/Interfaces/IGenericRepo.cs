using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    public interface IGenericRepo<T> where T : Base , new() //not abstract class like gymUser which is not a teble that has operations (bec abstarct classes cant have any constructor and does not have default constructor)
    {
        IEnumerable<T> GetAll(Func<T, bool>? predicate = null);

        T? GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
