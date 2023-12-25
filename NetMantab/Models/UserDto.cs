using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NetMantab.Models
{
    public class UserDto
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}