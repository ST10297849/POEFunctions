using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using System.Net.Http;

public static class BlobFunction
{
    [FunctionName("WriteToBlob")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient("product-images");
        await containerClient.CreateIfNotExistsAsync();

        var file = req.Form.Files["file"];
        using var stream = file.OpenReadStream();
        var blobClient = containerClient.GetBlobClient(file.FileName);
        await blobClient.UploadAsync(stream, true);

        return new OkObjectResult("File uploaded to blob storage.");
    }
   
}

