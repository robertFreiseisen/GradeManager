using Core.Contracts;

using System;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        public ISchoolClassRepository SchoolClassRepository { get; }
        public IGradeRepository GradeRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task CreateDatabaseAsync();
        Task MigrateDatabaseAsync();
    }
}
