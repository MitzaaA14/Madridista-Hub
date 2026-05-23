using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealMadridWeb.DTOs;
using RealMadridWeb.Services;

namespace RealMadridWeb.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayersApiController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersApiController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerReadDto>>> GetAllPlayers()
        {
            var players = await _playerService.GetAllPlayersAsync();
            return Ok(players);
        }

        [HttpGet("{id}", Name = "GetPlayerById")]
        public async Task<ActionResult<PlayerReadDto>> GetPlayerById(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreatePlayer([FromBody] PlayerCreateDto playerCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _playerService.CreatePlayerAsync(playerCreateDto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdatePlayer(int id, [FromBody] PlayerCreateDto playerUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _playerService.UpdatePlayerAsync(id, playerUpdateDto);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            var deleted = await _playerService.DeletePlayerAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}