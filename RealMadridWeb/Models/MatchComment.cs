using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RealMadridWeb.Models
{
    public class MatchComment
    {
        public int Id { get; set; }

        [Required]
        public int MatchId { get; set; }
        public Match? Match { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
