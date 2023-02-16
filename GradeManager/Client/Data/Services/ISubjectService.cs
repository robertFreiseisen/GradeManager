using Shared.Entities;

namespace Client.Data.Services
{
    public interface ISubjectService
    {
        public List<Subject> Subjects { get; set; }
        Task GetAllSubjectsAsync();
        Task GetSubjectsByTeacherAsync(int teacherId);
        Task DeleteSubjectAsync(int id);
    }
}
