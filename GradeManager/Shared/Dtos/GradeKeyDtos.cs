using Shared.Entities;
using Shared.Dtos;
/// <summary>
/// Dtos for GradeKeys
/// </summary>
namespace Shared.Dtos
{
    public record GradeKeyPostDto
    {
        public string Name { get; set; } = string.Empty;
        public List<GradeKindPostDto> UsedKinds { get; set; } = new List<GradeKindPostDto>();
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public ScriptType ScriptType { get; set; }
        public string Calculation { get; set; } = string.Empty;
    }
    
    public record GradeKeyGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GradeKindPostDto> UsedKinds { get; set; } = new List<GradeKindPostDto>();
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public ScriptType ScriptType { get; set; }
        public string Calculation { get; set; } = string.Empty;
    }
}