using System;
using System.Threading;
using Live360.Demo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Live360.Demo.Orchestration
{
    public static class FindFaceCount
    {
        /// <summary>
        /// Finds the face count in an image
        /// </summary>
        /// <param name="inputs">the input to the operation</param>
        /// <param name="logger">the logger</param>
        /// <returns>the count of detected faces</returns>
        [FunctionName(nameof(FindFaceCount))]
        public static int Run(
          [ActivityTrigger] ReceivedImageInfo input,
          ILogger logger)
        {
            if (Random.Shared.Next(10) == 0) throw new Exception("Buggy Library Exception");

            // Take up some memory
            var bytes = new byte[30_000_000];

            logger.LogInformation("Finding Face Count For Image: {Image}", JsonConvert.SerializeObject(input));

            // Simulate retrieving and processing the image
            Thread.Sleep(Random.Shared.Next(10_000, 30_000));

            // fake results
            var faceCount = Random.Shared.Next(0, 5);
            return faceCount;
        }
    }
}