using RealMadridWeb.DTOs.Staff;

namespace RealMadridWeb.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetAllAsync();
        Task<StaffDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateStaffDto dto);
        Task<bool> UpdateAsync(int id, UpdateStaffDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
