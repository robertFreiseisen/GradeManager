using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repos;

namespace Persistence
{
    
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext DbContext { get; }
        public ISchoolClassRepository SchoolClassRepository { get; }
        public IGradeRepository GradeRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public IGradeKeyRepository GradeKeyRepository { get; }
        public IGradeKindRepository GradeKindRepository { get; }
        public UnitOfWork()
        {
            DbContext = new ApplicationDbContext();
            SchoolClassRepository = new SchoolClassRepository(DbContext);
            GradeRepository = new GradeRepository(DbContext);
            StudentRepository= new StudentRepository(DbContext);
            SubjectRepository = new SubjectRepository(DbContext);
            GradeKeyRepository = new GradeKeyRepository(DbContext);
            GradeKindRepository= new GradeKindRepository(DbContext);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
        public async Task DeleteDatabaseAsync() => await DbContext.Database.EnsureDeletedAsync();
        public async Task CreateDatabaseAsync() => await DbContext.Database.EnsureCreatedAsync();
        public async Task MigrateDatabaseAsync()
        {
            var pending = await DbContext.Database.GetPendingMigrationsAsync();
            if (pending.Count() > 0)
            {
                await DbContext.Database.MigrateAsync();
            }
        }
        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
