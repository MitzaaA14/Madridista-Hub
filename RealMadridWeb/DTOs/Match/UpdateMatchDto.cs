using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.DTOs.Match
{
    public class UpdateMatchDto
    {
        [Required]
        [MaxLength(100)]
        public string Opponent { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string League { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Stadium { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(10)]
        public string Venue { get; set; } = "Home";

        public string? HomeTeamLogoUrl { get; set; }
        public string? AwayTeamLogoUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int? HomeScore { get; set; }

        [Range(0, int.MaxValue)]
        public int? AwayScore { get; set; }

        public bool IsFinished { get; set; }

        [Required]
        public int TeamId { get; set; }
    }
}
