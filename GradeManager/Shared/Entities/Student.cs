using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Student : EntityObject
    {
        public int SchoolClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        public string? Name { get; set; }
    }
}
