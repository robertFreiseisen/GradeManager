using Shared.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public enum ScriptType
    {
        None,
        Lua,
        Python,
        JavaScript,
        CSharpScript
    }
    public class GradeKey : EntityObject
    {
        public string? Calculation { get; set; }
        [ForeignKey("Teacher")]
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public Teacher? Teacher { get; set; }
        public string? Name { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public IEnumerable<SchoolClass>? SchoolClasses { get; set; }
        public IEnumerable<GradeKind> UsedKinds { get; set; }
        public ScriptType ScriptType { get; set; } = ScriptType.None;
    }
}