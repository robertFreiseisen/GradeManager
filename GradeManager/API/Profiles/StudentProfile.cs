using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {

            CreateMap<StudentPostDto, Student>().DisableCtorValidation()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => 0))
                .ForMember(
                    dest => dest.SchoolClassId,
                    opt => opt.MapFrom(src => src.SchoolClassId))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));

            CreateMap<Student, StudentGetDto>().DisableCtorValidation()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.SchoolClassId,
                    opt => opt.MapFrom(src => src.SchoolClassId))
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name));
        }
    }
}