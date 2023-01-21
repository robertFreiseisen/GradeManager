using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Persistence.Repos
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public ApplicationDbContext DbContext { get; }

        public GradeRepository(ApplicationDbContext context) : base(context)
        {
            DbContext = context;
        }

        public async Task AddGradeKeyAsync(GradeKey keyToAdd)
        {
            await DbContext.GradeKeys.AddAsync(keyToAdd);
        }

        public async Task<IEnumerable<GradeKey>> GetKeysByTeacherAsync(int TeacherId)
        {
            var result = await DbContext.GradeKeys.Where(k => k.TeacherId == TeacherId).ToArrayAsync();

            return result;
        }

        public async Task<IEnumerable<Grade>> GetByClassAndSubjectAsync(int schoolClassId, int subject)
        {
            return await DbContext.Grades
                .Include(g => g.Subject)
                .Include(g => g.Student)
                .Where(k => k.Student!.SchoolClassId == schoolClassId && k.SubjectId == subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetAllWithStudentsAsync()
        {
            return await DbContext.Grades.Include(g => g.Student).ToListAsync();
        }
    }
}
