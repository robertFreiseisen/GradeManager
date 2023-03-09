using Shared.Dtos;
using Shared.Entities;
using static System.Net.WebRequestMethods;

namespace Client.Data.Services
{
    public class SchoolCLassService
    {
        private readonly HttpClient _http;
        public List<SchoolClassGetDto> SchoolClasses { get; set; } = new List<SchoolClassGetDto>();
        public SchoolCLassService(HttpClient http) 
        {
            _http= http;
        }
        public async Task GetAllSchoolClassesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<SchoolClassGetDto>>("/schoolclasses");
            if (result != null)
            {
                SchoolClasses = result;
            }
        }
    }
}
