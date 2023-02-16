﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    [Serializable]
    public class GradeKind : EntityObject
    {
        public List<GradeKey> GradeKeys { get; set; } 
        public string Name { get; set; }
    }
}
