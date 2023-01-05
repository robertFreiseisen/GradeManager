using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SchoolClass : EntityObject 
    {
        public string Name { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public int SchoolLevel { get; set; }
        public DateTime SchoolYear { get; set; }
    }
}
