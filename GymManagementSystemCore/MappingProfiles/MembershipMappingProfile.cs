using AutoMapper;
using GymManagementSystemCore.ViewModels.MembershipViewModel;
using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.MappingProfiles
{
    internal class MembershipMappingProfile : Profile
    {
        public MembershipMappingProfile()
        {
            CreateMap<MemberPlan, MembershipViewModel>()
                .ForMember(MV => MV.MemberName, options => options.MapFrom(M => M.Member.Name))
                .ForMember(MV => MV.PlanName, options => options.MapFrom(M => M.Plan.Name))
                .ForMember(MV => MV.PlanStartDate, options => options.MapFrom(M => M.CreatedAt))
                .ForMember(MV => MV.PlanEndDate, options => options.MapFrom(M => M.EndDate));

            CreateMap<CreateMembershipViewModel, MemberPlan>()
                .ForMember(M => M.EndDate, options => options.Ignore());
        }
    }
}
