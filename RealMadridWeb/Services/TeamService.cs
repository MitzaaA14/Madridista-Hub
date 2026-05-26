using RealMadridWeb.DTOs.Team;
using RealMadridWeb.Models;
using RealMadridWeb.Repositories;

namespace RealMadridWeb.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly ILogger<TeamService> _logger;

        public TeamService(ITeamRepository repository, ILogger<TeamService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<TeamDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all teams.");
            var teams = await _repository.GetAllAsync();
            return teams.Select(ToDto);
        }

        public async Task<TeamDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching team with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Team with ID {Id} not found.", id);
                return null;
            }
            return ToDto(entity);
        }

        public async Task CreateAsync(CreateTeamDto dto)
        {
            _logger.LogInformation("Creating team {Name}.", dto.Name);
            var entity = new Team
            {
                Name = dto.Name,
                ImageUrl = dto.ImageUrl
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateTeamDto dto)
        {
            _logger.LogInformation("Updating team with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Update failed. Team with ID {Id} not found.", id);
                return false;
            }

            entity.Name = dto.Name;
            entity.ImageUrl = dto.ImageUrl;

            _repository.Update(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting team with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed. Team with ID {Id} not found.", id);
                return false;
            }
            _repository.Delete(entity);
            return await _repository.SaveChangesAsync();
        }

        private static TeamDto ToDto(Team t) => new()
        {
            Id = t.Id,
            Name = t.Name,
            ImageUrl = t.ImageUrl
        };
    }
}
