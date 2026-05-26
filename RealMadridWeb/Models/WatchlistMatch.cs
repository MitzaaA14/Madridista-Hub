using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RealMadridWeb.Models
{
    public class WatchlistMatch
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }

        [Required]
        public int MatchId { get; set; }
        public Match? Match { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
