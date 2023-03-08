using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    /// <summary>
    /// Defines Mapping for GradeKeys and GradeKeyDtos
    /// </summary>
    public class GradeKeyProfile : Profile
    {
        public GradeKeyProfile()
        {
            /// <summary>
            /// GradePostDto to DbGrade
            /// </summary>
            /// <typeparam name="GradeKeyPostDto"></typeparam>
            /// <typeparam name="GradeKey"></typeparam>
            /// <returns></returns>
            CreateMap<GradeKeyPostDto, GradeKey>().DisableCtorValidation()
            .ForMember(
                dest => dest.Id, 
                opt => opt.MapFrom(src => 0)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.ScriptType,
                opt => opt.MapFrom(src => src.ScriptType)
            )
            .ForMember(
                dest => dest.Calculation,
                opt => opt.MapFrom(src => src.Calculation)
            )
            .ForMember(
                dest => dest.SubjectId,
                opt => opt.MapFrom(src => src.SubjectId)
            )
            .ForMember(
                dest => dest.TeacherId,
                opt => opt.MapFrom(src => src.TeacherId)
            )
            .ForMember(
                dest => dest.UsedKinds,
                opt => opt.MapFrom(src => src.UsedKinds)
            );

            /// <summary>
            /// DbGradeKey to GradeKeyGetDto 
            /// </summary>
            /// <typeparam name="GradeKey"></typeparam>
            /// <typeparam name="GradeKeyGetDto"></typeparam>
            /// <returns></returns>
            CreateMap<GradeKey, GradeKeyGetDto>().DisableCtorValidation()
            .ForMember(
                dest => dest.Id, 
                opt => opt.MapFrom(src => src.Id)
            )
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name)
            )
            .ForMember(
                dest => dest.ScriptType,
                opt => opt.MapFrom(src => src.ScriptType)
            )
            .ForMember(
                dest => dest.Calculation,
                opt => opt.MapFrom(src => src.Calculation)
            )
            .ForMember(
                dest => dest.SubjectId,
                opt => opt.MapFrom(src => src.SubjectId)
            )
            .ForMember(
                dest => dest.TeacherId,
                opt => opt.MapFrom(src => src.TeacherId)
            )
            .ForMember(
                dest => dest.UsedKinds,
                opt => opt.MapFrom(src => src.UsedKinds)
            );
             
        }
    }
}