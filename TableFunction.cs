using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using System.Net.Http;

public static class TableFunction
{
    [FunctionName("StoreInTable")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        TableServiceClient serviceClient = new TableServiceClient(connectionString);
        TableClient tableClient = serviceClient.GetTableClient("CustomerProfiles");
        await tableClient.CreateIfNotExistsAsync();

        var profile = await req.ReadFromJsonAsync<CustomerProfile>();
        await tableClient.AddEntityAsync(profile);

        return new OkObjectResult("Customer profile stored in table.");
    }
   
}
