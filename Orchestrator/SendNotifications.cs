using System;
using System.Net.Http;
using System.Threading.Tasks;
using Live360.Demo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Live360.Demo.Orchestration
{
  public static class SendNotifications
  {

    private static HttpClient httpClient = new HttpClient();
    
    /// <summary>
    /// Send notifications about the results
    /// </summary>
    /// <param name="inputs">the input to the operation</param>
    /// <param name="logger">the logger</param>
    [FunctionName(nameof(SendNotifications))]
    public static async Task Run(
      [ActivityTrigger] ReceivedImageInfo input,
      ILogger logger)
    {
        // TODO: Send Notification

        logger.LogInformation("Sending Notification for Image: {Image}", JsonConvert.SerializeObject(input));

        // pretend 'Hello' is an external notification API call
        var hostname = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
        await httpClient.GetAsync($"https://{hostname}/api/Hello");
    }
  }
}