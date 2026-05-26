using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RealMadridWeb.Models
{
    public class FavoritePlayer
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; }

        [Required]
        public int PlayerId { get; set; }
        public Player? Player { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}
