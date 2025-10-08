using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemDAL.Repositories.Interfaces
{
    internal interface ITrainerRepo
    {
        IEnumerable<Trainer>? GetAllTrainers();

        Trainer? GetTrainerById(int id);

        int AddTrainer(Trainer trainer);

        int UpdateTrainer(Trainer trainer);

        int DeleteTrainer(Trainer trainer);
    }
}
