using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team?> GetByIdAsync(int id);
        Task AddAsync(Team team);
        void Update(Team team);
        void Delete(Team team);
        Task<bool> SaveChangesAsync();
    }
}
