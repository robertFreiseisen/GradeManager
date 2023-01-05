using Core.Contracts;
using Shared.Entities;

namespace Persistence.Repos
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public ApplicationDbContext DbContext { get; }
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            DbContext = context;
        }
    }
}
