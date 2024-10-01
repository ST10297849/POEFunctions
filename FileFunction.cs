using Azure.Storage.Files.Shares;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;

public static class FileFunction
{
    [FunctionName("WriteToFileShare")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var shareServiceClient = new ShareServiceClient(connectionString);
        var shareClient = shareServiceClient.GetShareClient("contracts-logs");
        await shareClient.CreateIfNotExistsAsync();

        var file = req.Form.Files["file"];
        using var stream = file.OpenReadStream();
        var rootDirectory = shareClient.GetRootDirectoryClient();
        var fileClient = rootDirectory.GetFileClient(file.FileName);
        await fileClient.CreateAsync(file.Length);
        await fileClient.UploadAsync(stream);

        return new OkObjectResult("File uploaded to Azure file share.");
    }
}

