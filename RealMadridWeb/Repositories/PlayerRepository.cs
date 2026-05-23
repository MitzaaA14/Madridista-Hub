using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.Include(p => p.Team).ToListAsync();
        }

        public async Task<Player?> GetPlayerByIdAsync(int id)
        {
            return await _context.Players.Include(p => p.Team).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPlayerAsync(Player player)
        {
            await _context.Players.AddAsync(player);
        }

        public void UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
        }

        public void DeletePlayer(Player player)
        {
            _context.Players.Remove(player);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}