using Microsoft.Extensions.Logging;
using RealMadridWeb.DTOs;
using RealMadridWeb.Models;
using RealMadridWeb.Repositories;

namespace RealMadridWeb.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(IPlayerRepository repository, ILogger<PlayerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<PlayerReadDto>> GetAllPlayersAsync()
        {
            _logger.LogInformation("Fetching all players from the database via repository.");
            var players = await _repository.GetAllPlayersAsync();
            return players.Select(p => new PlayerReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Position = p.Position,
                ShirtNumber = p.ShirtNumber,
                Goals = p.Goals,
                Assists = p.Assists,
                ImageUrl = p.ImageUrl,
                TeamId = p.TeamId,
                TeamName = p.Team != null ? p.Team.Name : string.Empty
            });
        }

        public async Task<PlayerReadDto?> GetPlayerByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching player with ID {id} from the database.");
            var p = await _repository.GetPlayerByIdAsync(id);
            if (p == null)
            {
                _logger.LogWarning($"Player with ID {id} was not found.");
                return null;
            }

            return new PlayerReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Position = p.Position,
                ShirtNumber = p.ShirtNumber,
                Goals = p.Goals,
                Assists = p.Assists,
                ImageUrl = p.ImageUrl,
                TeamId = p.TeamId,
                TeamName = p.Team != null ? p.Team.Name : string.Empty
            };
        }

        public async Task<int> CreatePlayerAsync(PlayerCreateDto dto)
        {
            _logger.LogInformation($"Creating a new player named {dto.Name}.");
            var player = new Player
            {
                Name = dto.Name,
                Position = dto.Position,
                ShirtNumber = dto.ShirtNumber,
                Goals = dto.Goals,
                Assists = dto.Assists,
                ImageUrl = dto.ImageUrl,
                TeamId = dto.TeamId
            };

            await _repository.AddPlayerAsync(player);
            await _repository.SaveChangesAsync();
            _logger.LogInformation($"Player {dto.Name} successfully created with internal ID {player.Id}.");
            return player.Id;
        }

        public async Task<bool> UpdatePlayerAsync(int id, PlayerCreateDto dto)
        {
            _logger.LogInformation($"Attempting to update player with ID {id}.");
            var player = await _repository.GetPlayerByIdAsync(id);
            if (player == null)
            {
                _logger.LogWarning($"Update failed. Player with ID {id} does not exist.");
                return false;
            }

            player.Name = dto.Name;
            player.Position = dto.Position;
            player.ShirtNumber = dto.ShirtNumber;
            player.Goals = dto.Goals;
            player.Assists = dto.Assists;
            player.ImageUrl = dto.ImageUrl;
            player.TeamId = dto.TeamId;

            _repository.UpdatePlayer(player);
            var result = await _repository.SaveChangesAsync();
            _logger.LogInformation($"Player with ID {id} was successfully updated.");
            return result;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            _logger.LogInformation($"Attempting to delete player with ID {id}.");
            var player = await _repository.GetPlayerByIdAsync(id);
            if (player == null)
            {
                _logger.LogWarning($"Delete failed. Player with ID {id} does not exist.");
                return false;
            }

            _repository.DeletePlayer(player);
            var result = await _repository.SaveChangesAsync();
            _logger.LogInformation($"Player with ID {id} was successfully removed from the system.");
            return result;
        }
    }
}