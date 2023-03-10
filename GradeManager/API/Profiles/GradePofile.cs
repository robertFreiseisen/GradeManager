using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    /// <summary>
    /// Defines the Mapping for Grades and GradeDtos
    /// </summary>
    public class GradePofile : Profile
    {
        public GradePofile()
        {
            /// <summary>
            /// Map from DbGradKind to GradeKindGetDto
            /// </summary>
            /// <typeparam name="GradeKind"></typeparam>
            /// <typeparam name="GradeKindGetDto"></typeparam>
            /// <returns></returns>
            CreateMap<GradeKind, GradeKindGetDto>().DisableCtorValidation()
            .ForMember(
                des => des.Id,
                opt => opt.MapFrom(src => src.Id)
                
            )
            .ForMember(
                des => des.Name,
                opt => opt.MapFrom(src => src.Name)
            );
            /// <summary>
            /// DbGrade to GradeGetDto
            /// </summary>
            /// <typeparam name="Grade"></typeparam>
            /// <typeparam name="GradeGetDto"></typeparam>
            /// <returns></returns>
            CreateMap<Grade, GradeGetDto>()
            .ForPath(
                dest => dest.StudentName,
                opt => opt.MapFrom(src => src.Student!.Name)
            )
            .ForPath(
                dest => dest.GradeKind,
                opt => opt.MapFrom(src => src.GradeKind.Name)
            );


            /// <summary>
            /// GradePostDto to Grade
            /// </summary>
            /// <typeparam name="GradePostDto"></typeparam>
            /// <typeparam name="Grade"></typeparam>
            /// <returns></returns>
            CreateMap<GradePostDto, Grade>().DisableCtorValidation()
            .ForMember(
                dest => dest.Id, 
                opt => opt.MapFrom(src => 0)
            )
            .ForPath(
                dest => dest.GradeKind.Name,
                opt => opt.MapFrom(src => src.GradeKind ) 
            );
            
        }
    }
}