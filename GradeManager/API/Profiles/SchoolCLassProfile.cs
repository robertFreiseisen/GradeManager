using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    public class SchoolCLassProfile : Profile
    {
        public SchoolCLassProfile() 
        {
            CreateMap<SchoolClassPostDto, SchoolClass>().DisableCtorValidation()              
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.Students,
                    opt => opt.MapFrom(src => src.Stutends))
                .ForMember(
                    dest => dest.SchoolLevel,
                    opt => opt.MapFrom(src => src.SchoolLevel))
                .ForMember(
                    dest => dest.SchoolYear,
                    opt => opt.MapFrom(src => src.SchoolYear));

            CreateMap<SchoolClass, SchoolClassGetDto>().DisableCtorValidation().
                ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => 0))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(
                    dest => dest.SchoolLevel,
                    opt => opt.MapFrom(src => src.SchoolLevel))
                .ForMember(
                    dest => dest.SchoolYear,
                    opt => opt.MapFrom(src => src.SchoolYear));
        }
    }
}
