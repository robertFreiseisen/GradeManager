﻿using System.ComponentModel.DataAnnotations.Schema;

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
    [Serializable]
    public class GradeKey : EntityObject
    {
        public string? Calculation { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public string Name { get; set; } = string.Empty;
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = new();
        public List<SchoolClass> SchoolClasses { get; set; } = new();
        public List<GradeKind> UsedKinds { get; set; } = new();
        public ScriptType ScriptType { get; set; } = ScriptType.None;
    }
}