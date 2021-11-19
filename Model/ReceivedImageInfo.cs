using Newtonsoft.Json;

namespace Live360.Demo.Models
{
    public class ReceivedImageInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("faceCount")]
        public int? FaceCount { get; set; }
    }
}