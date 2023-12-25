using Newtonsoft.Json;

namespace NetMantab.Models
{
    public class UserAndTodoDto {
        [JsonProperty("name")]
        public string? Name {get;set;}

        [JsonProperty("todos")]
        public List<UserAndTodoDto.Todo>? Todos {get; set;}
        
        public class Todo {
        [JsonProperty("title")]
            public string? Title {get;set;}

        [JsonProperty("description")]
            public string? Description {get;set;}

            public Todo(string title, string description) {
                Title = title;
                Description = description;
            }
        }
    }
}