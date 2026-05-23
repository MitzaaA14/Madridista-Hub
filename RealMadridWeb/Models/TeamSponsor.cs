using System.ComponentModel.DataAnnotations;

namespace RealMadridWeb.Models
{
    public class TeamSponsor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TeamId { get; set; }
        public virtual Team? Team { get; set; }

        [Required]
        public int SponsorId { get; set; }
        public virtual Sponsor? Sponsor { get; set; }
    }
}