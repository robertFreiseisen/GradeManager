﻿using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repos
{
    public class GradeKeyRepository : GenericRepository<GradeKey>, IGradeKeyRepository
    {
        public ApplicationDbContext DbContext { get; }
        public GradeKeyRepository(ApplicationDbContext context) : base(context)
        {
            DbContext = context;
        }

        public async Task<GradeKey?> GetByTeacherAndSubjectAsync(int teacherId, int subjectId)
        {
            return await DbContext.GradeKeys.SingleOrDefaultAsync(k => k.TeacherId == teacherId && k.SubjectId == subjectId);
        }
    }
}
