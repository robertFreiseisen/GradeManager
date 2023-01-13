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

        public async Task CreateGradeAsync(Grade grade)
        {
            var result = await _http.PostAsJsonAsync("api/grades", grade);
            await SetGradesAsync( result);
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

        public Task UpadateGradeKeyAsync(int keyId)
        {
            throw new NotImplementedException();
        }
    }
}
