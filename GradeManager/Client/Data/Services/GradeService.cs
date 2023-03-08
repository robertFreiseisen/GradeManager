using Client.Services;
using Microsoft.AspNetCore.Components;
using Shared.Dtos;
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

        public List<GradeKeyGetDto> GradeKeys { get; set; } = new List<GradeKeyGetDto>();
        public List<SchoolClassGetDto> Schooclasses { get; set;} = new List<SchoolClassGetDto>();
        public List<GradeGetDto> Grades { get; set; } = new List<GradeGetDto>();
        public List<GradeKindGetDto> Kinds { get; set; } = new List<GradeKindGetDto>();
        
        public async Task CreateGradeAsync(Grade grade)
        {
            var result = await _http.PostAsJsonAsync("/grades", grade);
            await SetGradesAsync(result);
        }

        private async Task SetGradesAsync(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<GradeKeyGetDto>();
        }

        public async Task CreateGradeKeyAsync(GradeKeyPostDto key)
        {
            var result = await _http.PostAsJsonAsync("addKey", key);
            await SetKey(result);
        }

        public async Task CreateGradeKeyAsync(GradeKeyPostDto key)
        {
            try
            {
                await _http.PostAsJsonAsync("/keys", key);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public Task DeleteGradeKeyAsync(int keyId)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllGradeKeysAsync()
        {
            var result = await _http.GetFromJsonAsync<List<GradeKeyGetDto>>("/keys");
            if (result != null)
            {
                GradeKeys = result;
            }
        }

        public async Task UpadateGradeKeyAsync(GradeKey gradeKey)
        {
            await _http.PostAsJsonAsync<GradeKey>("/keys", gradeKey);

            await this.GetAllGradeKeysAsync();
        }

        public GradeKeyGetDto? GetGradeKeyById(int? id)
        {
            return GradeKeys.SingleOrDefault(k => k.Id == id);
        }

        public async Task GetAllSchoolclassesAsync()
        {
            var schoolclasses = await _http.GetFromJsonAsync<List<SchoolClassGetDto>>("http://grades_backend/api/SchoolClass/GetAll");
            if (schoolclasses != null)
            {
                Schooclasses = schoolclasses;
            }
        }
        public async Task GetAllGradesAsync()
        {

            var result = await _http.GetFromJsonAsync<List<GradeGetDto>>("http://grades_backend/grades");
            if (result != null)
            {
                Grades = result;
            }
        }

        public async Task GetAllKindsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<GradeKindGetDto>>("/kinds");


            if (result != null)
            {
                Kinds = result;
            }
        }
    }
}
