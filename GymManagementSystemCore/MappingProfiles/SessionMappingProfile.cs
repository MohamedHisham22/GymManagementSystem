using AutoMapper;
using GymManagementSystemCore.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.MappingProfiles
{
    public class SessionMappingProfile : Profile
    {
        public SessionMappingProfile()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(SV => SV.CategoryName, options => options.MapFrom(S => S.Category.CategoryName))
                .ForMember(SV => SV.TrainerName, options => options.MapFrom(S => S.Trainer.Name))
                .ForMember(Sv => Sv.AvailableSlots, option => option.Ignore());

            CreateMap<CreateSessionViewModel , Session>();

            CreateMap<Session, UpdateSessionViewModel>();
            //.ReverseMap(); //Valid Both Ways

            CreateMap<UpdateSessionViewModel, Session>()
                .AfterMap((src, dest) => 
                {
                    dest.UpdatedAt = DateTime.Now;
                });
        }
    }
}
