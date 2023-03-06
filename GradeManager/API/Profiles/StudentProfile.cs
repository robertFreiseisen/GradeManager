using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentPostDto, Student>()
                .ForMember(
                    dest => dest.Id, 
                    opt => opt.MapFrom(src => 0))
                .ForMember(
                    dest => dest.SchoolClassId, 
                    opt => opt.MapFrom(src => src.SchoolClassId));
        }
    }
}