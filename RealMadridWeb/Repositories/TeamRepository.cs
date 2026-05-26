using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TeamRepository> _logger;

        public TeamRepository(ApplicationDbContext context, ILogger<TeamRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all teams");
            return await _context.Teams.Include(t => t.Players).ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(int id) =>
            await _context.Teams.Include(t => t.Players).Include(t => t.StaffMembers)
                .FirstOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(Team team) => await _context.Teams.AddAsync(team);
        public void Update(Team team) => _context.Teams.Update(team);
        public void Delete(Team team) => _context.Teams.Remove(team);
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
