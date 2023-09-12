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
        public string Name { get; set; } = string.Empty;
        public List<Student> Students { get; set; } = new();
        public int SchoolLevel { get; set; }
        public List<Teacher> Teachers { get; set; } = new();
        public string SchoolYear { get; set; } = "2022/23";
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
