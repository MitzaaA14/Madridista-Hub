using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.DTOs.Team
{
    public class UpdateTeamDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
    }
}
