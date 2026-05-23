using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty; // e.g., Manager, Assistant Coach, Physio

        [Required]
        public int TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}