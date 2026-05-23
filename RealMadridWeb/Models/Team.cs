using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<Staff> StaffMembers { get; set; } = new List<Staff>();
    }
}