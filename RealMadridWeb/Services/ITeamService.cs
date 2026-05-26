using RealMadridWeb.DTOs.Team;

namespace RealMadridWeb.Services
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllAsync();
        Task<TeamDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateTeamDto dto);
        Task<bool> UpdateAsync(int id, UpdateTeamDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
