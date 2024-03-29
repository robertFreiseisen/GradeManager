﻿using Shared.Entities;

namespace Client.Data.Services
{
    public interface IKindService
    {
        public List<GradeKind> GradeKinds { get; set; }
        Task GetAllKindsAsync();
        Task CreateKindAsync(GradeKind kind);
        Task DeleteKindAsync(int id);
    }
}
