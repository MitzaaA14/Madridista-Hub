using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task<Player?> GetPlayerByIdAsync(int id);
        Task AddPlayerAsync(Player player);
        void UpdatePlayer(Player player);
        void DeletePlayer(Player player);
        Task<bool> SaveChangesAsync();
    }
}