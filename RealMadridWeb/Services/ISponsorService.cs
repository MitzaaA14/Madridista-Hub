using RealMadridWeb.DTOs.Sponsor;

namespace RealMadridWeb.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<SponsorDto>> GetAllAsync();
        Task<SponsorDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateSponsorDto dto);
        Task<bool> UpdateAsync(int id, UpdateSponsorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
