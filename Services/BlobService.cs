using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace POEFunctions.Services
{
    public class BlobService
    {
        private readonly HttpClient _httpClient;

        public BlobService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task UploadBlobAsync(IFormFile file)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
            await _httpClient.PostAsync("https://function-url/WriteToBlob", content);
        }
    }
}
