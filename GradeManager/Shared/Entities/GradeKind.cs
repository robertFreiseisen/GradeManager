using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    [Serializable]
    public class GradeKind : EntityObject
    {
        public List<GradeKey> GradeKeys { get; set; } = new();
        public string Name { get; set; } = string.Empty;
    }

    public class GradeKindComparer : IEqualityComparer<GradeKind>
    {
        public bool Equals(GradeKind? x, GradeKind? y)
        {

            if (Object.ReferenceEquals(x, null) ||Object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] GradeKind obj)
        {
            if (Object.ReferenceEquals(obj, null))
            {
                return 0;
            }   

            int hashName = obj.Name == null ? 0 : obj.Name.GetHashCode();

            int hashId = obj.Id == 0 ? 0: obj.Id.GetHashCode();

            return hashName ^ hashId; 
        }
    }
}
