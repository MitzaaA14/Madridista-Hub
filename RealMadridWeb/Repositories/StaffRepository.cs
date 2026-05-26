using Microsoft.EntityFrameworkCore;
using RealMadridWeb.Data;
using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StaffRepository> _logger;

        public StaffRepository(ApplicationDbContext context, ILogger<StaffRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all staff");
            return await _context.Staff.Include(s => s.Team).ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id) =>
            await _context.Staff.Include(s => s.Team).FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(Staff staff) => await _context.Staff.AddAsync(staff);
        public void Update(Staff staff) => _context.Staff.Update(staff);
        public void Delete(Staff staff) => _context.Staff.Remove(staff);
        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}
