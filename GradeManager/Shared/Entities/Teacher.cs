using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    [Serializable()]
    public class Teacher : EntityObject
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public List<GradeKey> GradeKeys { get; set; } = new();

        public List<Subject> Subjects { get; set; } = new();
        public List<SchoolClass> SchoolClasses { get; set; } = new();
    }
}
