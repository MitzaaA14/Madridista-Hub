using RealMadridWeb.Models;
using RealMadridWeb.Repositories;

namespace RealMadridWeb.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IWatchlistRepository _watchlistRepo;
        private readonly IFavoritePlayerRepository _favoriteRepo;
        private readonly IMatchCommentRepository _commentRepo;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(
            IWatchlistRepository watchlistRepo,
            IFavoritePlayerRepository favoriteRepo,
            IMatchCommentRepository commentRepo,
            ILogger<ProfileService> logger)
        {
            _watchlistRepo = watchlistRepo;
            _favoriteRepo = favoriteRepo;
            _commentRepo = commentRepo;
            _logger = logger;
        }

        // ── Watchlist ──────────────────────────────────────────────────────────

        public Task<IEnumerable<WatchlistMatch>> GetWatchlistAsync(string userId) =>
            _watchlistRepo.GetByUserIdAsync(userId);

        public async Task<bool> AddToWatchlistAsync(string userId, int matchId)
        {
            var existing = await _watchlistRepo.GetAsync(userId, matchId);
            if (existing != null) return false; // deja adăugat

            await _watchlistRepo.AddAsync(new WatchlistMatch { UserId = userId, MatchId = matchId });
            await _watchlistRepo.SaveChangesAsync();
            _logger.LogInformation("User {UserId} added match {MatchId} to watchlist", userId, matchId);
            return true;
        }

        public async Task<bool> RemoveFromWatchlistAsync(string userId, int matchId)
        {
            var item = await _watchlistRepo.GetAsync(userId, matchId);
            if (item == null) return false;

            _watchlistRepo.Delete(item);
            await _watchlistRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsInWatchlistAsync(string userId, int matchId) =>
            await _watchlistRepo.GetAsync(userId, matchId) != null;

        // ── Favorite Players ───────────────────────────────────────────────────

        public Task<IEnumerable<FavoritePlayer>> GetFavoritePlayersAsync(string userId) =>
            _favoriteRepo.GetByUserIdAsync(userId);

        public async Task<bool> AddFavoritePlayerAsync(string userId, int playerId)
        {
            var existing = await _favoriteRepo.GetAsync(userId, playerId);
            if (existing != null) return false;

            await _favoriteRepo.AddAsync(new FavoritePlayer { UserId = userId, PlayerId = playerId });
            await _favoriteRepo.SaveChangesAsync();
            _logger.LogInformation("User {UserId} added player {PlayerId} to favorites", userId, playerId);
            return true;
        }

        public async Task<bool> RemoveFavoritePlayerAsync(string userId, int playerId)
        {
            var item = await _favoriteRepo.GetAsync(userId, playerId);
            if (item == null) return false;

            _favoriteRepo.Delete(item);
            await _favoriteRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsFavoritePlayerAsync(string userId, int playerId) =>
            await _favoriteRepo.GetAsync(userId, playerId) != null;

        // ── Comments ───────────────────────────────────────────────────────────

        public Task<IEnumerable<MatchComment>> GetCommentsByMatchAsync(int matchId) =>
            _commentRepo.GetByMatchIdAsync(matchId);

        public Task<IEnumerable<MatchComment>> GetCommentsByUserAsync(string userId) =>
            _commentRepo.GetByUserIdAsync(userId);

        public async Task<bool> AddCommentAsync(string userId, int matchId, string content)
        {
            if (string.IsNullOrWhiteSpace(content)) return false;

            await _commentRepo.AddAsync(new MatchComment
            {
                UserId = userId,
                MatchId = matchId,
                Content = content.Trim()
            });
            await _commentRepo.SaveChangesAsync();
            _logger.LogInformation("User {UserId} commented on match {MatchId}", userId, matchId);
            return true;
        }

        public async Task<bool> DeleteCommentAsync(string userId, int commentId)
        {
            var comment = await _commentRepo.GetByIdAsync(commentId);
            // Userul poate șterge doar propriile comentarii
            if (comment == null || comment.UserId != userId) return false;

            _commentRepo.Delete(comment);
            await _commentRepo.SaveChangesAsync();
            return true;
        }
    }
}
