using AutoMapper;
using GymManagementSystemCore.ViewModels.PlanViewModels;
using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.MappingProfiles
{
    public class PlanMappingProfile : Profile
    {
        public PlanMappingProfile()
        {
            CreateMap<Plan, PlanViewModel>();

            CreateMap<Plan, PlanToUpdateViewModel>();

            CreateMap<PlanToUpdateViewModel, Plan>()
                .AfterMap((src, dest) => 
                {
                    dest.UpdatedAt = DateTime.Now;
                });
        }
    }
}
