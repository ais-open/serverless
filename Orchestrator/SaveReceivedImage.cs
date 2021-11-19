using Live360.Demo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Live360.Demo.Orchestration
{
    public static class SaveReceivedImage
    {
        /// <summary>
        /// Save received image info to cosmos
        /// </summary>
        /// <param name="inputs">the input to the operation</param>
        /// <param name="logger">the logger</param>
        [FunctionName(nameof(SaveReceivedImage))]
        public static void Run(
          [ActivityTrigger] ReceivedImageInfo input,
          [CosmosDB(
            databaseName: "images",
            collectionName: "received",
            ConnectionStringSetting = "CosmosConnectionString")]
          out ReceivedImageInfo model,
          ILogger logger)
        {
            logger.LogInformation("Saving Image: {Image}", JsonConvert.SerializeObject(input));
            model = input;
        }
    }
}