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
            var result = await _http.PostAsJsonAsync<GradeKey>("grades/", key);
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

        public async Task UpadateGradeKeyAsync(GradeKey gradeKey)
        {
            await _http.PostAsJsonAsync<GradeKey>("http://grades_backend/keys", gradeKey);

            await this.GetAllGradeKeysAsync();
        }

        public GradeKey? GetGradeKeyById(int? id)
        {
            return GradeKeys.SingleOrDefault(k => k.Id == id);
        }
    }
}
