using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.DTOs.Staff
{
    public class CreateStaffDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        [Required]
        [MaxLength(100)]
        public string Role { get; set; } = string.Empty;

        public int? TeamId { get; set; }
    }
}
