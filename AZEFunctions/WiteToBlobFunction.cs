using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AZEFunctions
{
    public static class WriteToBlobFunction
    {
        [FunctionName("WriteToBlobFunction")]
        public static async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]Input input,
           [Blob("files/{Name}.txt", FileAccess.Write)] TextWriter fileStorage,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            fileStorage.WriteLine($"Hello from {input.Name} updated");

            return input.Name != null
                ? (ActionResult)new OkObjectResult($"Hello, {input.Name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        public class Input
        {
            public string Name { get; set; }
        }

        //[FunctionName("WriteToBlobFunction")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        //    [Blob("files/{name}.txt", FileAccess.Write)] TextWriter fileStorage,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    string name = req.Query["name"];

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    name = name ?? data?.name;

        //    fileStorage.WriteLine($"Hello from {name}");

        //    return name != null
        //        ? (ActionResult)new OkObjectResult($"Hello, {name}")
        //        : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        //}
    }
}
