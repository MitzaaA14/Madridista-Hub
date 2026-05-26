using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface ISponsorRepository
    {
        Task<IEnumerable<Sponsor>> GetAllAsync();
        Task<Sponsor?> GetByIdAsync(int id);
        Task AddAsync(Sponsor sponsor);
        void Update(Sponsor sponsor);
        void Delete(Sponsor sponsor);
        Task<bool> SaveChangesAsync();
    }
}
