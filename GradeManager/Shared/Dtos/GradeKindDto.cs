using Shared.Entities;

/// <summary>
/// Dtos for GradeKeys
/// </summary>
namespace Shared.Dtos
{
    public class GradeKindGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class GradeKindPostDto
    {
        public string Name { get; set; } = string.Empty;
    }
}