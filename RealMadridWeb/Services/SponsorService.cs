using RealMadridWeb.DTOs.Sponsor;
using RealMadridWeb.Models;
using RealMadridWeb.Repositories;

namespace RealMadridWeb.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _repository;
        private readonly ILogger<SponsorService> _logger;

        public SponsorService(ISponsorRepository repository, ILogger<SponsorService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<SponsorDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all sponsors.");
            var sponsors = await _repository.GetAllAsync();
            return sponsors.Select(ToDto);
        }

        public async Task<SponsorDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching sponsor with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Sponsor with ID {Id} not found.", id);
                return null;
            }
            return ToDto(entity);
        }

        public async Task CreateAsync(CreateSponsorDto dto)
        {
            _logger.LogInformation("Creating sponsor {Name}.", dto.Name);
            var entity = new Sponsor
            {
                Name = dto.Name,
                LogoUrl = dto.LogoUrl,
                Type = dto.Type
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateSponsorDto dto)
        {
            _logger.LogInformation("Updating sponsor with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Update failed. Sponsor with ID {Id} not found.", id);
                return false;
            }

            entity.Name = dto.Name;
            entity.LogoUrl = dto.LogoUrl;
            entity.Type = dto.Type;

            _repository.Update(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting sponsor with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed. Sponsor with ID {Id} not found.", id);
                return false;
            }
            _repository.Delete(entity);
            return await _repository.SaveChangesAsync();
        }

        private static SponsorDto ToDto(Sponsor s) => new()
        {
            Id = s.Id,
            Name = s.Name,
            LogoUrl = s.LogoUrl,
            Type = s.Type
        };
    }
}
