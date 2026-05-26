using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class SponsorRepository : ISponsorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SponsorRepository> _logger;

        public SponsorRepository(ApplicationDbContext context, ILogger<SponsorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all sponsors");
            return await _context.Sponsors.ToListAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(int id) =>
            await _context.Sponsors.FindAsync(id);

        public async Task AddAsync(Sponsor sponsor) => await _context.Sponsors.AddAsync(sponsor);
        public void Update(Sponsor sponsor) => _context.Sponsors.Update(sponsor);
        public void Delete(Sponsor sponsor) => _context.Sponsors.Remove(sponsor);
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
