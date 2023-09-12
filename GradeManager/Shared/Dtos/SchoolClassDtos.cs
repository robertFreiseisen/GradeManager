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
        public List<string> Stutends { get; set; } = new List<string>();
        public int SchoolLevel { get; set; }
        public string SchoolYear { get; set; } = string.Empty;
    }
    
    public class SchoolClassPostDto
    {
        public string Name { get; set; } = string.Empty;
        public List<string> Stutends { get; set; } = new List<string>();
        public int SchoolLevel { get; set; }
        public DateTime SchoolYear { get; set; }
    }
}