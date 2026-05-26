using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Models;

namespace RealMadridWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerMatch> PlayerMatches { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<TeamSponsor> TeamSponsors { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }

        // Profile features
        public DbSet<MatchComment> MatchComments { get; set; }
        public DbSet<WatchlistMatch> WatchlistMatches { get; set; }
        public DbSet<FavoritePlayer> FavoritePlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Unique: un user nu poate adăuga același meci de două ori în watchlist
            builder.Entity<WatchlistMatch>()
                .HasIndex(w => new { w.UserId, w.MatchId })
                .IsUnique();

            // Unique: un user nu poate favorita același jucător de două ori
            builder.Entity<FavoritePlayer>()
                .HasIndex(f => new { f.UserId, f.PlayerId })
                .IsUnique();
        }
    }
}
