using RealMadridWeb.DTOs.Match;
using RealMadridWeb.Models;
using RealMadridWeb.Repositories;

namespace RealMadridWeb.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _repository;
        private readonly ILogger<MatchService> _logger;

        public MatchService(IMatchRepository repository, ILogger<MatchService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<MatchDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all matches.");
            var matches = await _repository.GetAllAsync();
            return matches.Select(ToDto);
        }

        public async Task<IEnumerable<MatchDto>> GetUpcomingAsync()
        {
            _logger.LogInformation("Fetching upcoming matches.");
            var matches = await _repository.GetUpcomingAsync();
            return matches.Select(ToDto);
        }

        public async Task<MatchDto?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching match with ID {Id}.", id);
            var match = await _repository.GetByIdAsync(id);
            if (match == null)
            {
                _logger.LogWarning("Match with ID {Id} not found.", id);
                return null;
            }
            return ToDto(match);
        }

        public async Task CreateAsync(CreateMatchDto dto)
        {
            _logger.LogInformation("Creating match vs {Opponent}.", dto.Opponent);
            var entity = new Match
            {
                Opponent = dto.Opponent,
                League = dto.League,
                Stadium = dto.Stadium,
                Date = dto.Date,
                Venue = dto.Venue,
                HomeTeamLogoUrl = dto.HomeTeamLogoUrl,
                AwayTeamLogoUrl = dto.AwayTeamLogoUrl,
                HomeScore = dto.HomeScore,
                AwayScore = dto.AwayScore,
                IsFinished = dto.IsFinished,
                TeamId = dto.TeamId
            };
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateMatchDto dto)
        {
            _logger.LogInformation("Updating match with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Update failed. Match with ID {Id} not found.", id);
                return false;
            }

            entity.Opponent = dto.Opponent;
            entity.League = dto.League;
            entity.Stadium = dto.Stadium;
            entity.Date = dto.Date;
            entity.Venue = dto.Venue;
            entity.HomeTeamLogoUrl = dto.HomeTeamLogoUrl;
            entity.AwayTeamLogoUrl = dto.AwayTeamLogoUrl;
            entity.HomeScore = dto.HomeScore;
            entity.AwayScore = dto.AwayScore;
            entity.IsFinished = dto.IsFinished;
            entity.TeamId = dto.TeamId;

            _repository.Update(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting match with ID {Id}.", id);
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Delete failed. Match with ID {Id} not found.", id);
                return false;
            }
            _repository.Delete(entity);
            return await _repository.SaveChangesAsync();
        }

        private static MatchDto ToDto(Match m) => new()
        {
            Id = m.Id,
            Opponent = m.Opponent,
            League = m.League,
            Stadium = m.Stadium,
            Date = m.Date,
            Venue = m.Venue,
            HomeTeamLogoUrl = m.HomeTeamLogoUrl,
            AwayTeamLogoUrl = m.AwayTeamLogoUrl,
            HomeScore = m.HomeScore,
            AwayScore = m.AwayScore,
            IsFinished = m.IsFinished,
            TeamId = m.TeamId,
            TeamName = m.Team?.Name ?? string.Empty
        };
    }
}
