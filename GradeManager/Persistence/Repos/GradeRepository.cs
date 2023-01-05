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
    }
}
