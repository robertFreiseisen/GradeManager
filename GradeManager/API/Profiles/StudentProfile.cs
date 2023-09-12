using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    /// <summary>
    /// Definiton of Mapping for StudentDTOs
    /// </summary>
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {

            CreateMap<StudentPostDto, Student>().DisableCtorValidation()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => 0));

            CreateMap<Student, StudentGetDto>().DisableCtorValidation();
        }
    }
}