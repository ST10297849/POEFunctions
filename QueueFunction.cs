using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;

public static class QueueFunction
{
    [FunctionName("QueueTransaction")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var queueServiceClient = new QueueServiceClient(connectionString);
        var queueClient = queueServiceClient.GetQueueClient("transaction-queue");
        await queueClient.CreateIfNotExistsAsync();

        var transactionMessage = await req.ReadAsStringAsync();
        await queueClient.SendMessageAsync(transactionMessage);

        return new OkObjectResult("Message sent to queue.");
    }
}

