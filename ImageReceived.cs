using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading.Tasks;
using Live360.Demo.Models;
using Live360.Demo.Orchestration;
using Newtonsoft.Json;

namespace Live360.Demo
{
    public static class ImageReceived
    {
        [FunctionName(nameof(ImageReceived))]
        public static async Task Run(
            [EventGridTrigger] 
            EventGridEvent eventGridEvent,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            dynamic data = eventGridEvent.Data;

            var uri = new Uri((string)data.url);
            var fileName = uri.PathAndQuery.Split('/')[2];

            // Function input comes from the request content.
            var info = new ReceivedImageInfo
            {
                Id = eventGridEvent.Id,
                FileName = fileName
            };

            string instanceId = await starter.StartNewAsync(nameof(ImageOrchestrator), info);

            log.LogInformation("Started orchestratio for Image: {Image} with Orch ID: '{OrchestrationId}'.", 
                JsonConvert.SerializeObject(info), instanceId);
        }
    }
}
