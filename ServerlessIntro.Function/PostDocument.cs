using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.ServiceBus;
using System.Text;
using Microsoft.Azure.WebJobs.ServiceBus;

namespace ServerlessIntro.Function
{
    public static class PostDocument
    {
        [FunctionName("PostDocument")]
        public static async Task<IActionResult> Run(
                [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
                [ServiceBus("documents", Connection = "ServiceBus:ConnectionString", EntityType = EntityType.Queue)] IAsyncCollector<Message> documentsQueue,
                ILogger log)
        {
            log.LogInformation(new EventId(101), "Document received");
            string document = new StreamReader(req.Body).ReadToEnd();
            await documentsQueue.AddAsync(new Message(Encoding.ASCII.GetBytes(document)));
            log.LogInformation(new EventId(102), "Document sent to queue");
            return new OkResult();
        }
    }
}
