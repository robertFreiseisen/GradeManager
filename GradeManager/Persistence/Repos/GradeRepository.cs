using Core.Contracts;
using Core.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public ApplicationDbContext DbContext { get; }

        public GradeRepository(ApplicationDbContext context) : base(context)
        {
            DbContext = context;
        }
    }
}
