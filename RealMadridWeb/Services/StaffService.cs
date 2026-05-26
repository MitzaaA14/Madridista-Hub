using RealMadridWeb.DTOs.Staff;
using RealMadridWeb.Models;
using RealMadridWeb.Repositories;

namespace RealMadridWeb.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _repository;
        private readonly ILogger<StaffService> _logger;

        public StaffService(IStaffRepository repository, ILogger<StaffService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<StaffDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all staff.");
            var staff = await _repository.GetAllAsync();
            return staff.Select(ToDto);
        }

        public async Task<StaffDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching staff member with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Staff member with ID {Id} not found.", id);
                return null;
            }
            return ToDto(entity);
        }

        public async Task CreateAsync(CreateStaffDto dto)
        {
            _logger.LogInformation("Creating staff member {Name}.", dto.Name);
            var entity = new Staff
            {
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
                Role = dto.Role,
                TeamId = dto.TeamId
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateStaffDto dto)
        {
            _logger.LogInformation("Updating staff member with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Update failed. Staff member with ID {Id} not found.", id);
                return false;
            }

            entity.Name = dto.Name;
            entity.ImageUrl = dto.ImageUrl;
            entity.Role = dto.Role;
            entity.TeamId = dto.TeamId;

            _repository.Update(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting staff member with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed. Staff member with ID {Id} not found.", id);
                return false;
            }
            _repository.Delete(entity);
            return await _repository.SaveChangesAsync();
        }

        private static StaffDto ToDto(Staff s) => new()
        {
            Id = s.Id,
            Name = s.Name,
            ImageUrl = s.ImageUrl,
            Role = s.Role,
            TeamId = s.TeamId,
            TeamName = s.Team?.Name
        };
    }
}
