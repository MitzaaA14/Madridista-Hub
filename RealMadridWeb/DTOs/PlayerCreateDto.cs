using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.DTOs
{
    public class PlayerCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Range(1, 99)]
        public int ShirtNumber { get; set; }

        public int Goals { get; set; } = 0;
        public int Assists { get; set; } = 0;

        [Required]
        public int TeamId { get; set; }
    }
}