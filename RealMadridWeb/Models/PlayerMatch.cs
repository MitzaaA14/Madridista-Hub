using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class PlayerMatch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlayerId { get; set; }
        public virtual Player? Player { get; set; }

        [Required]
        public int MatchId { get; set; }
        public virtual Match? Match { get; set; }

        public int MinutesPlayed { get; set; }

        public double Rating { get; set; }
    }
}