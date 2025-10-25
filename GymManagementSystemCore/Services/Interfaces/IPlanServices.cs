using GymManagementSystemCore.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.Services.Interfaces
{
    public interface IPlanServices
    {
        public IEnumerable<PlanViewModel> GetAllPlans();

        public PlanViewModel? GetPlanDetails(int id);
        public PlanToUpdateViewModel? PlanToUpdate(int id);
        public bool UpdatePlanDetails(int id, PlanToUpdateViewModel plan);

        public bool TogglePlanActiveStatus(int id);

    }
}
