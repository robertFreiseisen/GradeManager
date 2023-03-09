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
                des => des.SchoolClasses, 
                opt => opt.MapFrom(src => src.SchoolClasses.Select(c => new SchoolClass{ Name = c }))
            )
            .ForMember(
                dest => dest.UsedKinds,
                opt => opt.MapFrom(src => src.UsedKinds.Select(k => new GradeKind { Name = k }))
            );

            /// <summary>
            /// DbGradeKey to GradeKeyGetDto 
            /// </summary>
            /// <typeparam name="GradeKey"></typeparam>
            /// <typeparam name="GradeKeyGetDto"></typeparam>
            /// <returns></returns>
            CreateMap<GradeKey, GradeKeyGetDto>().DisableCtorValidation()
            .ForPath(
                dest => dest.UsedKinds,
                opt => opt.MapFrom(src => src.UsedKinds.Select(k => k.Name))
            )
            .ForMember(
                dest => dest.SchoolClasses,
                opt => opt.MapFrom(src => src.SchoolClasses!.Select(sc => sc.Name))
            );
        }
    }
}