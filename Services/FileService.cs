using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace POEFunctions.Services
{
    public class FileService
    {
        private readonly HttpClient _httpClient;

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
            await _httpClient.PostAsync("https://your-function-url/WriteToFileShare", content);
        }
    }
}

