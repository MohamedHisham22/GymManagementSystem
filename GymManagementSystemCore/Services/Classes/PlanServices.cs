using AutoMapper;
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
    public class PlanServices : IPlanServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var planRepo = _unitOfWork.GetRepo<Plan>();
            var plans = planRepo.GetAll();

            if (plans is null || !plans.Any()) return [];

            return _mapper.Map<IEnumerable<Plan>, IEnumerable<PlanViewModel>>(plans);

        }

        public PlanViewModel? GetPlanDetails(int id)
        {
            var plan = _unitOfWork.GetRepo<Plan>().GetById(id);
            if (plan is null) return null;
            return _mapper.Map<Plan , PlanViewModel>(plan);
        }

        public PlanToUpdateViewModel? PlanToUpdate(int id)
        {
            var plan = _unitOfWork.GetRepo<Plan>().GetById(id);
            if(plan is null) return null;
            var IsmemberShipsActive = _unitOfWork.GetRepo<MemberPlan>().GetAll(MP => MP.PlanId == plan.Id && MP.Status == "Active").Any();
            if (plan.IsActive || IsmemberShipsActive) return null;
            return _mapper.Map<Plan, PlanToUpdateViewModel>(plan);
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

                _mapper.Map(planView, plan);
                _unitOfWork.GetRepo<Plan>().Update(plan);
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
