using Core.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Teacher : EntityObject
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<GradeKey>? GradeKeys { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<SchoolClass>? SchoolClasses { get; set; }
    }
}
