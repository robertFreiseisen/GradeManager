using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace Shared.Entities
{
    public class Subject : EntityObject
    {
        public string Name { get; set; } = string.Empty;
    }
}