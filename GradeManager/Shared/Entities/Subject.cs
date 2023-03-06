using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace Shared.Entities
{
    [Serializable()]
    public class Subject : EntityObject
    {
        public string Name { get; set; }
    }
}