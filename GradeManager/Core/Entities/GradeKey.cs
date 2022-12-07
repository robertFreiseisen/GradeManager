using Core.Contracts.Entities;

namespace Core.Entities
{
    public enum ScriptType
    {
        None,
        Lua,
        Python,
        JavaScript,
        CSharpScript
    }
    public class GradeKey
    {
        public string? Calculation { get; set; }
        public string? Name { get; set; }
        public IEnumerable<Grade>? Grades { get; set; }
        public ScriptType ScriptType { get; set; } = ScriptType.None;
    }
}