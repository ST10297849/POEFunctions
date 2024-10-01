using System.Net.Http;
using System.Threading.Tasks;

namespace POEFunctions.Services
{
    public class QueueService
    {
        private readonly HttpClient _httpClient;

        public QueueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendMessageAsync(string message)
        {
            var response = await _httpClient.PostAsync("https://your-function-url/QueueTransaction", new StringContent(message));
        }
    }
}
