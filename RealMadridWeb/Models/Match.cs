using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public string Opponent { get; set; } = string.Empty;

        [Required]
        public string League { get; set; } = string.Empty;

        [Required]
        public string Stadium { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public string Venue { get; set; } = "Home"; 

        public string? HomeTeamLogoUrl { get; set; }
        public string? AwayTeamLogoUrl { get; set; }

        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public bool IsFinished { get; set; }

        [Required]
        [Display(Name = "Club Team")]
        public int TeamId { get; set; }
        public Team? Team { get; set; }
    }
}