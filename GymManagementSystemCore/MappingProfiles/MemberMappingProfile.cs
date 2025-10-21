using AutoMapper;
using GymManagementSystemCore.ViewModels.MemberViewModels;
using GymManagementSystemDAL.Models;
using GymManagementSystemDAL.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemCore.MappingProfiles
{
    public class MemberMappingProfile : Profile
    {
        public MemberMappingProfile()
        {
            CreateMap<Member , MemberViewModel>()
                .ForMember(MV=>MV.Gender , options => options.MapFrom(M=>M.Gender.ToString()));

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(M => M.Address, options => options.MapFrom(MV => new Address()
                {
                    BuildingNumber = MV.BuildingNumber,
                    Street = MV.Street,
                    City = MV.City,
                }
                ))
                .ForMember(M => M.HealthRecord, options => options.MapFrom(MV=> new HealthRecord()
                {
                    Height = MV.HealthRecord.Height, //no need for loading beacuse its mapped from view model 
                    Weight = MV.HealthRecord.Weight, //no need for loading beacuse its mapped from view model 
                    BloodType = MV.HealthRecord.BloodType, //no need for loading beacuse its mapped from view model 
                    Note = MV.HealthRecord.Note, //no need for loading beacuse its mapped from view model 
                }
                ));

            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(MV => MV.Gender, options => options.MapFrom(M => M.Gender.ToString()))
                .ForMember(MV => MV.Address, options => options.MapFrom(M => $"{M.Address.BuildingNumber}, {M.Address.Street}, {M.Address.City}")) //loaded because address and member is one table
                .ForMember(MV => MV.DateOfBirth, options => options.MapFrom(M => M.DateOfBirth.ToShortDateString()));

            CreateMap<HealthRecord, MemberHealthRecordViewModel>();

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(MV => MV.BuildingNumber, options => options.MapFrom(M => M.Address.BuildingNumber))
                .ForMember(MV => MV.Street, options => options.MapFrom(M => M.Address.Street))
                .ForMember(MV => MV.City, options => options.MapFrom(M => M.Address.City))
                .ReverseMap();

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(M => M.Name, options => options.Ignore()) //this field is for display only and shouldnt take a value , if its left non ignored it will be mapped
                .ForMember(M => M.Photo, options => options.Ignore()) //this field is for display only and shouldnt take a value , if its left non ignored it will be mapped
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber; //Here We Update The Address And Its Object Is Already Created Before So No Need To Create New Object
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                    dest.UpdatedAt = DateTime.Now; //Better Assign UpdatedAt Just Before Save Changes For An Accurate Result
                });
        }
    }
}
