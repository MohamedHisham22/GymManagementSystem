using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.TrainerViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.BaseClasses;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Classes
{
    internal class TrainerServices : ITrainerServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateTrainer(CreateTrainerViewModel trainerView)
        {
            try 
            {
                if (IsEmailExists(email: trainerView.Email) || IsPhoneExists(trainerView.Phone)) return false;

                Trainer trainer = new Trainer()
                {
                    Name = trainerView.Name,
                    Email = trainerView.Email,
                    Phone = trainerView.Phone,
                    DateOfBirth = trainerView.DateOfBirth,
                    Gender = trainerView.Gender,
                    Address = new Address() 
                    {
                        BuildingNumber = trainerView.BuildingNumber,
                        Street = trainerView.Street,
                        City = trainerView.City,
                    },
                    Speciality = trainerView.Specialization,
                };
                
                _unitOfWork.GetRepo<Trainer>().Add(trainer);

                return _unitOfWork.saveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }

        public bool DeleteTrainer(int id)
        {
            try
            {
                var trainerRepo = _unitOfWork.GetRepo<Trainer>();
                var trainer = trainerRepo.GetById(id);
                if (trainer == null) return false;

                #region Check Trainer Has Active Sessions
                var HasActiveSessions = _unitOfWork.GetRepo<Session>().GetAll(S => S.TrainerID == id && S.StartDate > DateTime.Now).Any();
                if (HasActiveSessions) return false;
                #endregion

                trainerRepo.Delete(trainer);
                return _unitOfWork.saveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepo<Trainer>().GetAll();
            if (trainers == null || !trainers.Any()) return [];

            return trainers.Select(T => new TrainerViewModel() 
            {
                Id = T.Id,
                Name = T.Name,
                Email = T.Email,
                Phone = T.Phone,
                Specialization = T.Speciality.ToString(),
            });
        }

        public TrainerDetailsViewModel? GetTrainerDetailsById(int id)
        {
            var trainer = _unitOfWork.GetRepo<Trainer>().GetById(id);
            if (trainer == null) return null;
            return new TrainerDetailsViewModel()
            {
                Email = trainer.Email,
                Phone = trainer.Phone,
                DateOfBirth = trainer.DateOfBirth.ToString(),
                Address = $"{trainer.Address.BuildingNumber}, {trainer.Address.Street}, {trainer.Address.City}",
            };

        }

        public TrainerToUpdateViewModel? TrainerToUpdate(int id)
        {

            var trainer = _unitOfWork.GetRepo<Trainer>().GetById(id);
            if (trainer == null) return null;

            return new TrainerToUpdateViewModel() 
            {
               Name = trainer.Name,
               Email = trainer.Email,
               BuildingNumber = trainer.Address.BuildingNumber,
               Street = trainer.Address.Street,
               Specialization = trainer.Speciality,
            };

        }

        public bool UpdateTrainer(int id, TrainerToUpdateViewModel trainerView)
        {
            try
            {
                var trainerRepo = _unitOfWork.GetRepo<Trainer>();
                var trainer = trainerRepo.GetById(id);
                if (trainer == null) return false;

                if (trainer.Email != trainerView.Email && IsEmailExists(trainerView.Email)) return false;

                (trainer.Email, trainer.Address.BuildingNumber, trainer.Address.Street, trainer.Speciality , trainer.UpdatedAt)
                    = (trainerView.Email, trainerView.BuildingNumber, trainerView.Street, trainerView.Specialization , DateTime.Now);


                trainerRepo.Update(trainer);
                return _unitOfWork.saveChanges() > 0;

            }
            catch
            {
                return false;
            }
        }

        #region Helpers
        public bool IsEmailExists(string email) => _unitOfWork.GetRepo<Trainer>().GetAll(T => T.Email == email).Any();
        public bool IsPhoneExists(string phone) => _unitOfWork.GetRepo<Trainer>().GetAll(T => T.Phone == phone).Any();

        #endregion
    }
}
