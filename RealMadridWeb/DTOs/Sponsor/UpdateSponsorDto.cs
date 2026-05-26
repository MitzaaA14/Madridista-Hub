using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.DTOs.Sponsor
{
    public class UpdateSponsorDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = "Main Partner";
    }
}
