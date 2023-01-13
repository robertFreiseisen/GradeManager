using Core.Contracts;
using Shared.Entities;

namespace Persistence.Repos
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public ApplicationDbContext DbContext { get; }
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            DbContext = context;
        }
    }
}
