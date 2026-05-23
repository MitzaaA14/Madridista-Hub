using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Sponsor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        [Required]
        public string Type { get; set; } = "Main Partner"; 
    }
}