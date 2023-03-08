using Shared.Dtos;
using Shared.Entities;

namespace Client.Services
{
    public interface IGradeService
    {
        Task GetAllGradesAsync();
        Task GetAllKindsAsync();
        public List<GradeGetDto> Grades { get; set;}
        public List<GradeKindGetDto> Kinds { get; set; }
        public List<SchoolClassGetDto> Schooclasses { get; set; }
        public List<GradeKeyGetDto> GradeKeys { get; set; }
        //Task CreateGradeAsync(Grade grade);
        //Task GetAllGradesAsync();
        GradeKeyGetDto? GetGradeKeyById(int? id);
        Task GetAllSchoolclassesAsync();
        Task GetAllGradeKeysAsync();
        Task CreateGradeKeyAsync(GradeKeyPostDto key);
        Task DeleteGradeKeyAsync(int keyId);
        //Task UpdateGradeAsync(int gradeId);
        //Task DeleteGradeAsync(int gradeId);
    }
}
