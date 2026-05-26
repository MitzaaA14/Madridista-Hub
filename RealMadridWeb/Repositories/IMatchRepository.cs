using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface IMatchRepository
    {
        Task<IEnumerable<Match>> GetAllAsync();
        Task<Match?> GetByIdAsync(int id);
        Task<IEnumerable<Match>> GetUpcomingAsync();
        Task AddAsync(Match match);
        void Update(Match match);
        void Delete(Match match);
        Task<bool> SaveChangesAsync();
    }
}
