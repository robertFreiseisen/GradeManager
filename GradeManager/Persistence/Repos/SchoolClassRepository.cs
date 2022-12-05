using Core.Contracts;
using Core.Contracts.Entities;

namespace Persistence.Repos
{
    public class SchoolClassRepository : GenericRepository<SchoolClass>, ISchoolClassRepository
    {
        public ApplicationDbContext DbContext { get; }
        public SchoolClassRepository(ApplicationDbContext context) : base(context)
        {
            DbContext= context; 
        }
    }
}
