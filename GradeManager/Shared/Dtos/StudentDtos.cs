using Shared.Entities;

/// <summary>
/// Dtos for Students
/// </summary>
namespace Shared.Dtos
{
    public class StudentGetDto
    {
        public int Id {get; set;}
        public int SchoolClassId { get; set; } 
        public string Name { get; set; } = string.Empty;
    }

    public class StudentPostDto
    {
        public string Name { get; set; } = string.Empty;
        public int SchoolClassId { get; set; }
    }
}