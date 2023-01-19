using Client.Services;
using Microsoft.AspNetCore.Components;
using Shared.Entities;


namespace Client.Data.Services
{
    public class GradeService : IGradeService
    {
        private readonly HttpClient _http;
        public GradeService(HttpClient http)
        {
            _http = http;
        }

        public List<GradeKey> GradeKeys { get; set; } = new List<GradeKey>();
        public List<Grade> Grades { get; set; } = new List<Grade>();
        public List<SchoolClass> Schooclasses { get; set;} = new List<SchoolClass>();

        public async Task CreateGradeAsync(Grade grade)
        {
            var result = await _http.PostAsJsonAsync("api/grades", grade);
            await SetGradesAsync(result);
        }

        private async Task SetGradesAsync(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<Grade>();
        }

        public async Task CreateGradeKeyAsync(GradeKey key)
        {
            var result = await _http.PostAsJsonAsync<GradeKey>("http://grades_backend/keys/", key);
            await SetKey(result);
        }

        private async Task SetKey(HttpResponseMessage result)
        {
            var res = await result.Content.ReadFromJsonAsync<GradeKey>();
            GradeKeys.Add(res!);
        }

        public Task DeleteGradeKeyAsync(int keyId)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllGradeKeysAsync()
        {
            var result = await _http.GetFromJsonAsync<List<GradeKey>>("http://grades_backend/keys");
            if (result != null)
            {
                GradeKeys = result;
            }
        }
        public async Task GetAllGradesAsync()
        {
            var result = await _http.GetFromJsonAsync<List<Grade>>("http://grades_backend/grades");
            if (result != null)
            {
                Grades = result;
            }
        }

        public async Task UpadateGradeKeyAsync(GradeKey gradeKey)
        {
            await _http.PostAsJsonAsync<GradeKey>("http://grades_backend/keys", gradeKey);

            await this.GetAllGradeKeysAsync();
        }

        public GradeKey? GetGradeKeyById(int? id)
        {
            return GradeKeys.SingleOrDefault(k => k.Id == id);
        }

        public async Task GetAllSchoolclassesAsync()
        {
            var schoolclasses = await _http.GetFromJsonAsync<List<SchoolClass>>("http://grades_backend/api/schoolclasses");
            if (schoolclasses != null)
            {
                Schooclasses = schoolclasses;
            }
        }
    }
}
