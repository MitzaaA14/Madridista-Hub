using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string League { get; set; } = string.Empty;

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();

        public virtual ICollection<Staff> StaffMembers { get; set; } = new List<Staff>();

        public virtual ICollection<TeamSponsor> TeamSponsors { get; set; } = new List<TeamSponsor>();
    }
}