using Client.Services;
using Microsoft.AspNetCore.Components;
using Shared.Entities;

namespace Client.Data.Services
{
    public class GradeService : IGradeService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<GradeKey> GradeKeys { get; set; } = new List<GradeKey>();

        /*public async Task CreateGradeAsync(Grade grade)
        {
            var result = await _http.PostAsJsonAsync("api/grades", grade);
            await SetGradesAsync( result);
        }

        private async Task SetGradesAsync(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<Grade>();
            _navigationManager.NavigateTo("grades");
        }*/
        
        public async Task CreateGradeKeyAsync(GradeKey key)
        {
            var result = await _http.PostAsJsonAsync<GradeKey>("api/grades/keys", key);
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
            var result = await _http.GetFromJsonAsync<List<GradeKey>>("api/grades/keys");
            if (result != null)
            {
                GradeKeys = result;
            }
        }

        public Task UpadteGradeKeyAsync(int keyId)
        {
            throw new NotImplementedException();
        }
    }
}
