using GymManagementSystemCore.Services.Interfaces;
using GymManagementSystemCore.ViewModels.PlanViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Classes
{
    internal class PlanServices : IPlanServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var planRepo = _unitOfWork.GetRepo<Plan>();
            var plans = planRepo.GetAll();

            if (plans is null || !plans.Any()) return [];

            return plans.Select(P => new PlanViewModel()
            {
                Id = P.Id,
                Name = P.Name,
                Description = P.Description,
                DurationDays = P.DurationDays,
                Price = P.Price,
                isActive = P.IsActive,
            });

        }

        public PlanViewModel? GetPlanDetails(int id)
        {
            var plan = _unitOfWork.GetRepo<Plan>().GetById(id);
            if (plan is null) return null;
            return new PlanViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                isActive = plan.IsActive,
            };
        }

        public PlanToUpdateViewModel? PlanToUpdate(int id)
        {
            var plan = _unitOfWork.GetRepo<Plan>().GetById(id);
            var IsmemberShipsActive = _unitOfWork.GetRepo<MemberPlan>().GetAll(MP => MP.PlanId == plan.Id && MP.Status == "Active").Any();
            if (plan is null || plan.IsActive || IsmemberShipsActive) return null;
            var planView = new PlanToUpdateViewModel()
            {
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
            };
            return planView;
        }

        public bool UpdatePlanDetails(int id, PlanToUpdateViewModel planView)
        {
            try 
            {
                var plan = _unitOfWork.GetRepo<Plan>().GetById(id);
                if (plan == null) return false;

                #region Check If Plan Has Active MemberShip
                var IsmemberShipsActive = _unitOfWork.GetRepo<MemberPlan>().GetAll(MP=>MP.PlanId == plan.Id && MP.Status =="Active").Any();
                if (IsmemberShipsActive) return false;
                #endregion

                (plan.Name, plan.Description, plan.DurationDays, plan.Price , plan.UpdatedAt)
                =
                (planView.Name, planView.Description, planView.DurationDays, planView.Price , DateTime.Now);

                return _unitOfWork.saveChanges() > 0;
            }
            catch 
            {
                return false;
            }

        }

        public bool TogglePlanActiveStatus(int id)
        {
            try 
            {
                var plan = _unitOfWork.GetRepo<Plan>().GetById(id);
                if (plan == null) return false;

                #region Check If Plan Is Active
                var IsmemberShipsActive = _unitOfWork.GetRepo<MemberPlan>().GetAll(MP => MP.Id == plan.Id && MP.Status == "Active").Any();
                if (IsmemberShipsActive) return false;
                #endregion
                
                plan.IsActive = !plan.IsActive;
                plan.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepo<Plan>().Update(plan);
                return _unitOfWork.saveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }
    }
}
