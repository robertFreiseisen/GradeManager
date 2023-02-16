using Shared.Entities;

namespace Client.Data.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly HttpClient _http;
        public SubjectService(HttpClient http)
        {
            _http = http;
        }

        public List<Subject> Subjects { get; set; } = new List<Subject>();

        public Task DeleteSubjectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllSubjectsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Subject>>("subjects");

            if (result != null)
            {
                Subjects = result;
            }
        }
        private async Task SetSubjectAsync(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<Subject>();
        }

        public async Task GetSubjectsByTeacherAsync(int teacherId)
        {

            var result = await _http.GetFromJsonAsync<List<Subject>>($"teacher/{teacherId}");

            if (result != null)
            {
                Subjects = result;
            }
        }
    }
}