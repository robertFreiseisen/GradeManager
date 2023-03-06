using Shared.Entities;

/// <summary>
/// Dtos for GradeKeys
/// </summary>
namespace Shared.Dtos
{
    public record GradeKindGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public record GradeKindPostDto
    {
        public string Name { get; set; } = string.Empty;
    }
}