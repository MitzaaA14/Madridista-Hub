using RealMadridWeb.DTOs.Match;

namespace RealMadridWeb.Services
{
    public interface IMatchService
    {
        Task<IEnumerable<MatchDto>> GetAllAsync();
        Task<IEnumerable<MatchDto>> GetUpcomingAsync();
        Task<MatchDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateMatchDto dto);
        Task<bool> UpdateAsync(int id, UpdateMatchDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
