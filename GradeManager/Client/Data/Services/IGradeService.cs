using Shared.Entities;

namespace Client.Services
{
    public interface IGradeService
    {
        Task GetAllGradesAsync();
        public List<Grade> Grades { get; set;}
        public List<SchoolClass> Schooclasses { get; set; }
        public List<GradeKey> GradeKeys { get; set; }
        //Task CreateGradeAsync(Grade grade);
        //Task GetAllGradesAsync();
        GradeKey? GetGradeKeyById(int? id);
        Task GetAllSchoolclassesAsync();
        Task GetAllGradeKeysAsync();
        Task CreateGradeKeyAsync(GradeKey key);
        Task UpadateGradeKeyAsync(GradeKey keyId);
        Task DeleteGradeKeyAsync(int keyId);
        //Task UpdateGradeAsync(int gradeId);
        //Task DeleteGradeAsync(int gradeId);
    }
}
