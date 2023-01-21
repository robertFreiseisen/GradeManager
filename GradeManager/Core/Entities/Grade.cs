﻿using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Entities
{
    public class Grade : EntityObject 
    {
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int GradeNumber { get; set; }
        public string? Note { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
