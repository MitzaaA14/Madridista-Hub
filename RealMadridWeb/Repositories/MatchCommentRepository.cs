using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class MatchCommentRepository : IMatchCommentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MatchCommentRepository> _logger;

        public MatchCommentRepository(ApplicationDbContext context, ILogger<MatchCommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<MatchComment>> GetByMatchIdAsync(int matchId)
        {
            _logger.LogInformation("Fetching comments for match {MatchId}", matchId);
            return await _context.MatchComments
                .Include(c => c.User)
                .Where(c => c.MatchId == matchId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<MatchComment>> GetByUserIdAsync(string userId) =>
            await _context.MatchComments
                .Include(c => c.Match).ThenInclude(m => m!.Team)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

        public async Task<MatchComment?> GetByIdAsync(int id) =>
            await _context.MatchComments.FindAsync(id);

        public async Task AddAsync(MatchComment comment) =>
            await _context.MatchComments.AddAsync(comment);

        public void Delete(MatchComment comment) =>
            _context.MatchComments.Remove(comment);

        public async Task<bool> SaveChangesAsync() =>
            await _context.SaveChangesAsync() > 0;
    }
}
