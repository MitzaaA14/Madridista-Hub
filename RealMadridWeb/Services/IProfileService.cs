using RealMadridWeb.Models;

namespace RealMadridWeb.Services
{
    public interface IProfileService
    {
        Task<IEnumerable<WatchlistMatch>> GetWatchlistAsync(string userId);
        Task<bool> AddToWatchlistAsync(string userId, int matchId);
        Task<bool> RemoveFromWatchlistAsync(string userId, int matchId);
        Task<bool> IsInWatchlistAsync(string userId, int matchId);

        Task<IEnumerable<FavoritePlayer>> GetFavoritePlayersAsync(string userId);
        Task<bool> AddFavoritePlayerAsync(string userId, int playerId);
        Task<bool> RemoveFavoritePlayerAsync(string userId, int playerId);
        Task<bool> IsFavoritePlayerAsync(string userId, int playerId);

        Task<IEnumerable<MatchComment>> GetCommentsByMatchAsync(int matchId);
        Task<IEnumerable<MatchComment>> GetCommentsByUserAsync(string userId);
        Task<bool> AddCommentAsync(string userId, int matchId, string content);
        Task<bool> DeleteCommentAsync(string userId, int commentId);
    }
}
