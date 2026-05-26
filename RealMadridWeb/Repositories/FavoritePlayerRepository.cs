using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class FavoritePlayerRepository : IFavoritePlayerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FavoritePlayerRepository> _logger;

        public FavoritePlayerRepository(ApplicationDbContext context, ILogger<FavoritePlayerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<FavoritePlayer>> GetByUserIdAsync(string userId)
        {
            _logger.LogInformation("Fetching favorite players for user {UserId}", userId);
            return await _context.FavoritePlayers
                .Include(f => f.Player).ThenInclude(p => p!.Team)
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Player!.Name)
                .ToListAsync();
        }

        public async Task<FavoritePlayer?> GetAsync(string userId, int playerId) =>
            await _context.FavoritePlayers
                .FirstOrDefaultAsync(f => f.UserId == userId && f.PlayerId == playerId);

        public async Task AddAsync(FavoritePlayer item) =>
            await _context.FavoritePlayers.AddAsync(item);

        public void Delete(FavoritePlayer item) =>
            _context.FavoritePlayers.Remove(item);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
