using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repos;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext DbContext { get; }

        public ISchoolClassRepository SchoolClassRepository => throw new NotImplementedException();

        public IGradeRepository GradeRepository => throw new NotImplementedException();

        public IStudentRepository StudentRepository => throw new NotImplementedException();

        public ISubjectRepository SubjectRepository => throw new NotImplementedException();

        public UnitOfWork()
        {
            DbContext = new ApplicationDbContext();

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
