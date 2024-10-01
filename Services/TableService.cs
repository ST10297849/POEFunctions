using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace POEFunctions.Services
{
    public class TableService
    {
        private readonly HttpClient _httpClient;

        public TableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddEntityAsync(CustomerProfile profile)
        {
            var response = await _httpClient.PostAsJsonAsync("https://your-function-url/StoreInTable", profile);
        }
    }
}

