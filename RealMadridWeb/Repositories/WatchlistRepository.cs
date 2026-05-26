using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WatchlistRepository> _logger;

        public WatchlistRepository(ApplicationDbContext context, ILogger<WatchlistRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<WatchlistMatch>> GetByUserIdAsync(string userId)
        {
            _logger.LogInformation("Fetching watchlist for user {UserId}", userId);
            return await _context.WatchlistMatches
                .Include(w => w.Match).ThenInclude(m => m!.Team)
                .Where(w => w.UserId == userId)
                .OrderBy(w => w.Match!.Date)
                .ToListAsync();
        }

        public async Task<WatchlistMatch?> GetAsync(string userId, int matchId) =>
            await _context.WatchlistMatches
                .FirstOrDefaultAsync(w => w.UserId == userId && w.MatchId == matchId);

        public async Task AddAsync(WatchlistMatch item) =>
            await _context.WatchlistMatches.AddAsync(item);

        public void Delete(WatchlistMatch item) =>
            _context.WatchlistMatches.Remove(item);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
