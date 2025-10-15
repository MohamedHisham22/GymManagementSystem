using GymManagementSystemCore.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Interfaces
{
    internal interface ITrainerServices
    {
        public IEnumerable<TrainerViewModel> GetAllTrainers();

        public bool CreateTrainer(CreateTrainerViewModel trainerView);

        public TrainerDetailsViewModel? GetTrainerDetailsById(int id);

        public TrainerToUpdateViewModel? TrainerToUpdate(int id);

        public bool UpdateTrainer(int id, TrainerToUpdateViewModel trainerView);

        public bool DeleteTrainer(int id);


    }
}
