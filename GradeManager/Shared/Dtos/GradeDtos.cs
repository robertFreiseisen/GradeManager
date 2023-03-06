using Shared.Entities;

/// <summary>
/// Dtos for GradeKeys
/// </summary>
namespace Shared.Dtos
{
    public class GradeGetDto
    {
        public int Id { get; set; } 
        public string GradeKind { get; set;} = string.Empty;
        public int SubjectId { get; set; }
        public int TeacherId { get; set; } 
        public int StudentId { get; set; } 
        public string? Note { get; set; } = string.Empty;
    }

    public class GradePostDto
    {
        public string GradeKind { get; set;} = string.Empty;
        public int SubjectId { get; set; }
        public int TeacherId { get; set; } 
        public int StudentId { get; set; } 
        public string? Note { get; set; } = string.Empty;
    }
}