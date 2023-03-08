using Shared.Entities;

/// <summary>
/// Dtos for SchoolClass
/// </summary>
namespace Shared.Dtos
{
    public class SchoolClassGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<StudentGetDto> Stutends { get; set; } = new List<StudentGetDto>();
        public int SchoolLevel { get; set; }
        public DateTime SchoolYear { get; set; }
    }
    
    public class SchoolClassPostDto
    {
        public string Name { get; set; } = string.Empty;
        public List<StudentPostDto> Stutends { get; set; } = new List<StudentPostDto>();
        public int SchoolLevel { get; set; }
        public DateTime SchoolYear { get; set; }
    }
}