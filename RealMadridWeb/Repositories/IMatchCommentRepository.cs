using RealMadridWeb.Models;

namespace RealMadridWeb.Repositories
{
    public interface IMatchCommentRepository
    {
        Task<IEnumerable<MatchComment>> GetByMatchIdAsync(int matchId);
        Task<IEnumerable<MatchComment>> GetByUserIdAsync(string userId);
        Task<MatchComment?> GetByIdAsync(int id);
        Task AddAsync(MatchComment comment);
        void Delete(MatchComment comment);
        Task<bool> SaveChangesAsync();
    }
}
