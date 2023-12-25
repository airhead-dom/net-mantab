using System.ComponentModel.DataAnnotations.Schema;

namespace NetMantab.Models
{
    public class User
    {
        public int ID { get; set; }

        [Column("name")]
        public string? Name { get; set; }
    }
}