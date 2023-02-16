using Shared.Entities;
using static System.Net.WebRequestMethods;

namespace Client.Data.Services
{
    public class SchoolCLassService
    {
        private readonly HttpClient _http;
        public List<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();
        public SchoolCLassService(HttpClient http) 
        {
            _http= http;
        }
        public async Task GetAllSchoolClassesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<SchoolClass>>("http://grades_backend/api/schoolclasses");
            if (result != null)
            {
                SchoolClasses = result;
            }
        }
    }
}
