using Shared.Entities;

namespace Client.Data.Services
{
    public class KindService : IKindService
    {

        HttpClient _http;

        public KindService(HttpClient http)
        {
            _http = http;
        }
        public List<GradeKind> GradeKinds { get; set; } = new List<GradeKind>();

        public Task CreateKindAsync(GradeKind kind)
        {
            throw new NotImplementedException();
        }

        public Task DeleteKindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllKindsAsync()
        {
            var result = await _http.GetFromJsonAsync<List<GradeKind>>("kinds");
            if (result != null)
            {
                GradeKinds = result;
            }
        }
    }
}