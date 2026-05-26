using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface IFavoritePlayerRepository
    {
        Task<IEnumerable<FavoritePlayer>> GetByUserIdAsync(string userId);
        Task<FavoritePlayer?> GetAsync(string userId, int playerId);
        Task AddAsync(FavoritePlayer item);
        void Delete(FavoritePlayer item);
        Task<bool> SaveChangesAsync();
    }
}
