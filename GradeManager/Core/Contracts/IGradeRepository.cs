﻿using Shared.Entities;

namespace Core.Contracts
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Task AddGradeKeyAsync(GradeKey keyToAdd);
        Task<IEnumerable<GradeKey>> GetKeysByTeacherAsync(int TeacherId);
    }
}
