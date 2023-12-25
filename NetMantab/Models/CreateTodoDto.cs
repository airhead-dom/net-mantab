using Newtonsoft.Json;

namespace NetMantab.Models
{
    public class CreateTodoDto
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("ownedBy")]
        public int OwnedBy { get; set; }
    }
}