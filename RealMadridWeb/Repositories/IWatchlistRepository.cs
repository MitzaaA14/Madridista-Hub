using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface IWatchlistRepository
    {
        Task<IEnumerable<WatchlistMatch>> GetByUserIdAsync(string userId);
        Task<WatchlistMatch?> GetAsync(string userId, int matchId);
        Task AddAsync(WatchlistMatch item);
        void Delete(WatchlistMatch item);
        Task<bool> SaveChangesAsync();
    }
}
