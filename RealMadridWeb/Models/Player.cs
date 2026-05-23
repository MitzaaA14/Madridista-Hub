using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealMadridWeb.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Position { get; set; } = string.Empty;

        [Required]
        [Range(1, 99)]
        public int ShirtNumber { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int TeamId { get; set; }

        [ForeignKey("TeamId")]
        public Team? Team { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Goals { get; set; } = 0;

        [Required]
        [Range(0, int.MaxValue)]
        public int Assists { get; set; } = 0;
    }
}