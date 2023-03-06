using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    [Serializable()]
    public class Grade : EntityObject 
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        public Student? Student { get; set; }
        
        [Required]
        public int Graduate { get; set; }
        public string? Note { get; set; }
        
        [Required]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        [Required]
        public Subject? Subject { get; set; }
        
        [ForeignKey("Teacher")]
        [Required]
        public int TeacherId { get; set; }
        
        [Required]
        public Teacher? Teacher { get; set; }

        [ForeignKey("GradeKind")]
        [Required]
        public int GradeKindId { get; set; }
        [Required]
        public GradeKind GradeKind { get; set; } // Test oder MAK, Hausuebung
    }
}
