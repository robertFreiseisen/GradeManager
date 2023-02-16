using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class EntityObject : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Timestamp]
        public byte[]? RowVersion
        {
            get;
            set;
        }
    }
}
