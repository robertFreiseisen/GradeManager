using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    public class SchoolClassProfile : Profile
    {
        public SchoolClassProfile() 
        {
            CreateMap<SchoolClassPostDto, SchoolClass>().DisableCtorValidation()     
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => 0)
                )
                .ForMember(
                    dest => dest.Students,
                    opt => opt.MapFrom(src => src.Stutends.Select(s => new Student { Name = s}))
                );

            CreateMap<SchoolClass, SchoolClassGetDto>().DisableCtorValidation()
                .ForMember(
                    dest => dest.Stutends,
                    opt => opt.MapFrom(src => src.Students.Select(s => s.Name))
                );
        }
    }
}
