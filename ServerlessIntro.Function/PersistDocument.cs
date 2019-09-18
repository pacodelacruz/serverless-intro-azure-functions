using System;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Extensions.Logging;

namespace ServerlessIntro.Function
{
    public static class PersistDocument
    {
        [FunctionName("PersistDocument")]
        public static void Run(
                [ServiceBusTrigger("documents", Connection = "ServiceBus:ConnectionString")] Message document,
                [CosmosDB("ServerlessIntroDb", "Documents", ConnectionStringSetting = "CosmosDB:ConnectionString")] out dynamic outDocument
                , ILogger log)
        {
            log.LogInformation(new EventId(121), $"ServiceBus queue triggered function");
            outDocument = System.Text.Encoding.Default.GetString(document.Body);
            log.LogInformation(new EventId(122), $"Document inserted in CosmosDb collection");
        }
    }
}
