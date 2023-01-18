using Shared.Entities;

namespace Client.Services
{
    public interface IGradeService
    {
        public List<GradeKey> GradeKeys { get; set; }
        public List<Grade> Grades { get; set; }
        public List<SchoolClass> Schooclasses { get; set; }

        //Task CreateGradeAsync(Grade grade);
        //Task GetAllGradesAsync();
        Task GetAllGradeKeysAsync();
        Task CreateGradeKeyAsync(GradeKey key);
        Task UpadateGradeKeyAsync(int keyId);
        Task DeleteGradeKeyAsync(int keyId);
        Task GetAllGradesAsync();
        Task GetAllSchoolclassesAsync();
        //Task UpdateGradeAsync(int gradeId);
        //Task DeleteGradeAsync(int gradeId);
    }
}
