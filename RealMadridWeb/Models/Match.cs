using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Opponent { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        public string Stadium { get; set; } = string.Empty;

        public virtual ICollection<PlayerMatch> PlayerMatches { get; set; } = new List<PlayerMatch>();
    }
}