using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    [Serializable()]
    public class SchoolClass : EntityObject 
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public int SchoolLevel { get; set; }
        public List<Teacher> Teachers { get; set; }
        public DateTime SchoolYear { get; set; }
    }

    public class SchoolClassComparer : IEqualityComparer<SchoolClass>
    {
        public bool Equals(SchoolClass? x, SchoolClass? y)
        {
            return x!.Name == y!.Name;
        }

        public int GetHashCode([DisallowNull] SchoolClass obj)
        {
            return obj.GetHashCode();
        }
    }
}
