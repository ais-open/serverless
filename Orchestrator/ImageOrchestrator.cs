using System.Threading.Tasks;
using Live360.Demo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Live360.Demo.Orchestration
{
    public static class ImageOrchestrator
    {
        [FunctionName(nameof(ImageOrchestrator))]
        public static async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {
            var imageInfo = context.GetInput<ReceivedImageInfo>();

            var log = context.CreateReplaySafeLogger(logger);
            log.LogInformation("Begin Orchestration for Image: {Image}", JsonConvert.SerializeObject(imageInfo));

            // 1) Save Input Data to Cosmos
            log.LogTrace("Calling Save Activity for {Image}", JsonConvert.SerializeObject(imageInfo));
            await context.CallActivityAsync(nameof(SaveReceivedImage), imageInfo);

            // 2) Find Faces
            log.LogTrace("Calling Find Face Activity for {Image}", JsonConvert.SerializeObject(imageInfo));
            imageInfo.FaceCount = await context.CallActivityAsync<int>(nameof(FindFaceCount), imageInfo);

            // 3) Save the results - SAME METHOD
            log.LogTrace("Calling Save Activity for {Image}", JsonConvert.SerializeObject(imageInfo));
            await context.CallActivityAsync(nameof(SaveReceivedImage), imageInfo);

            // 4) Send notifications
            log.LogTrace("Calling Send Notifications for {Image}", JsonConvert.SerializeObject(imageInfo));
            await context.CallActivityAsync(nameof(SendNotifications), imageInfo);

            log.LogDebug("Orchestration Done for {Image}", JsonConvert.SerializeObject(imageInfo));
        }
    }
}