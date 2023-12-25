using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace NetMantab.Models;
public class Todo
{
    public int ID { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int Order { get; set; }

    [Required]
    public int OwnedBy { get; set; }
}