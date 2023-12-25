using Newtonsoft.Json;

namespace NetMantab.Models
{
    public class TodoDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }

        [JsonProperty("ownedBy")]
        public int OwnedBy { get; set; }
    }
}