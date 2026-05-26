using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MatchRepository> _logger;

        public MatchRepository(ApplicationDbContext context, ILogger<MatchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all matches");
            return await _context.Matches.Include(m => m.Team).ToListAsync();
        }

        public async Task<Match?> GetByIdAsync(int id) =>
            await _context.Matches.Include(m => m.Team).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<IEnumerable<Match>> GetUpcomingAsync() =>
            await _context.Matches
                .Include(m => m.Team)
                .Where(m => !m.IsFinished && m.Date >= DateTime.UtcNow)
                .OrderBy(m => m.Date)
                .ToListAsync();

        public async Task AddAsync(Match match) => await _context.Matches.AddAsync(match);
        public void Update(Match match) => _context.Matches.Update(match);
        public void Delete(Match match) => _context.Matches.Remove(match);
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
