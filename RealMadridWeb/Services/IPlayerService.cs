using RealMadridWeb.DTOs;

namespace RealMadridWeb.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerReadDto>> GetAllPlayersAsync();
        Task<PlayerReadDto?> GetPlayerByIdAsync(int id);
        Task CreatePlayerAsync(PlayerCreateDto playerCreateDto);
        Task<bool> UpdatePlayerAsync(int id, PlayerCreateDto playerUpdateDto);
        Task<bool> DeletePlayerAsync(int id);
    }
}