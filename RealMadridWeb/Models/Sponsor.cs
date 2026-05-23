using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class Sponsor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty; // e.g., Main Kit Sponsor, Global Partner

        public virtual ICollection<TeamSponsor> TeamSponsors { get; set; } = new List<TeamSponsor>();
    }
}