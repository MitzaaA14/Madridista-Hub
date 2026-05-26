namespace RealMadridWeb.Models
{
    public class ProfileViewModel
    {
        public string Email { get; set; } = string.Empty;
        public IEnumerable<WatchlistMatch> Watchlist { get; set; } = new List<WatchlistMatch>();
        public IEnumerable<FavoritePlayer> FavoritePlayers { get; set; } = new List<FavoritePlayer>();
        public IEnumerable<MatchComment> RecentComments { get; set; } = new List<MatchComment>();
    }
}
