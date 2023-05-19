using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
