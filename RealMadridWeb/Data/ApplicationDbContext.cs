using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Models;

namespace RealMadridWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerMatch> PlayerMatches { get; set; }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TeamSponsor> TeamSponsors { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}