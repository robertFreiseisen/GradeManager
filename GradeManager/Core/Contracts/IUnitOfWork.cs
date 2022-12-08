using Shared;

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
        public IGradeKeyRepository GradeKeyRepository { get; }
        public IGradeKindRepository GradeKindRepository { get; }

        Task<int> SaveChangesAsync();
        Task DeleteDatabaseAsync();
        Task CreateDatabaseAsync();
        Task MigrateDatabaseAsync();
    }
}
