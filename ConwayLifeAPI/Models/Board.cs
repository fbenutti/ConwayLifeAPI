using System.ComponentModel.DataAnnotations;

namespace ConwayLifeAPI.Models
{
    public class Board
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string StateJson { get; set; } = string.Empty;

        public int Generation { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
