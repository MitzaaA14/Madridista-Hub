using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Staff
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;

        public int? TeamId { get; set; }
        public Team? Team { get; set; }
    }
}