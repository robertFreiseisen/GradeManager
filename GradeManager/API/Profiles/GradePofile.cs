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
            .ForMember(
                dest => dest.Id, 
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Note,
                opt => opt.MapFrom(src => src.Note)
            )
            .ForMember(
                dest => dest.StudentId, 
                opt => opt.MapFrom(src => src.StudentId)
            )
            .ForMember(
                dest => dest.SubjectId,
                opt => opt.MapFrom(src => src.SubjectId)
            )
            .ForMember(
                dest => dest.TeacherId,
                opt => opt.MapFrom(src => src.TeacherId)
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
            .ForMember(
                dest => dest.Note,
                opt => opt.MapFrom(src => src.Note)
            )
            .ForMember(
                dest => dest.StudentId, 
                opt => opt.MapFrom(src => src.StudentId)
            )
            .ForMember(
                dest => dest.SubjectId,
                opt => opt.MapFrom(src => src.SubjectId)
            )
            .ForMember(
                dest => dest.TeacherId,
                opt => opt.MapFrom(src => src.TeacherId)
            )
            .ForPath(
                dest => dest.GradeKind.Name,
                opt => opt.MapFrom(src => src.GradeKind ) 
            );
            
        }
    }
}