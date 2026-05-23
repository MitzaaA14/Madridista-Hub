using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        public int ShirtNumber { get; set; }

        public int Goals { get; set; } = 0;
        public int Assists { get; set; } = 0;

        [Required]
        public int TeamId { get; set; }

        public virtual Team? Team { get; set; }

        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; } = new List<PlayerMatch>();
    }
}