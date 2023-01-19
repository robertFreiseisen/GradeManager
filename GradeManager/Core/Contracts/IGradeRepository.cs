using Shared.Entities;

namespace Core.Contracts
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Task AddGradeKeyAsync(GradeKey keyToAdd);
        Task<IEnumerable<GradeKey>> GetKeysByTeacherAsync(int TeacherId);
        Task<IEnumerable<Grade>> GetByClassAndSubjectAsync(int schoolClassId, int subject);
        Task<IEnumerable<Grade>> GetAllWithStudentsAsync();
    }
}
