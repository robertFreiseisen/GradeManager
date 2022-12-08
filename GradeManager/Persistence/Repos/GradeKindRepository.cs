using Core.Contracts;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
    public class GradeKindRepository : GenericRepository<GradeKind>, IGradeKindRepository
    {
        public ApplicationDbContext DbContext { get; }
        public GradeKindRepository(ApplicationDbContext context) : base(context)
        {
            DbContext = context;
        }
    }
}
