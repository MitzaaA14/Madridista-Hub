using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task AddAsync(Staff staff);
        void Update(Staff staff);
        void Delete(Staff staff);
        Task<bool> SaveChangesAsync();
    }
}
