using Newtonsoft.Json;

namespace NetMantab.Models
{
    public class CreateUserDto
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}