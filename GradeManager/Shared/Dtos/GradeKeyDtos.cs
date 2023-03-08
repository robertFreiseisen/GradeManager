using Shared.Entities;
using Shared.Dtos;
/// <summary>
/// Dtos for GradeKeys
/// </summary>
namespace Shared.Dtos
{
    [Serializable()]
    public class GradeKeyPostDto
    {
        public string Name { get; set; } = string.Empty;
        public List<string> SchoolClasses { get; set; } = new List<string>();
        public List<GradeKindPostDto> UsedKinds { get; set; } = new List<GradeKindPostDto>();
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public ScriptType ScriptType { get; set; }
        public string Calculation { get; set; } = string.Empty;
    }
    
    public class GradeKeyGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<string> UsedKinds { get; set; } = new List<string>();
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public ScriptType ScriptType { get; set; }
        public List<string> SchoolClasses { get; set; } = new List<string>();
        public string Calculation { get; set; } = string.Empty;
    }
}