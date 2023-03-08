using AutoMapper;
using Shared.Dtos;
using Shared.Entities;

namespace API.Profiles
{
    /// <summary>
    /// Defines the Mapping for GradeKinds and GradeKindsDtos
    /// </summary>
    public class GradeKindProfile : Profile
    {
        public GradeKindProfile()
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
            /// Map from GradKindPostDto to DbGradeKind
            /// </summary>
            /// <typeparam name="GradeKind"></typeparam>
            /// <typeparam name="GradeKindGetDto"></typeparam>
            /// <returns></returns>
            CreateMap<GradeKindPostDto, GradeKind>().DisableCtorValidation()
            .ForMember(
                des => des.Id,
                opt => opt.MapFrom(src => 0)
                
            )
            .ForMember(
                des => des.Name,
                opt => opt.MapFrom(src => src.Name)
            );
        }
    }
}