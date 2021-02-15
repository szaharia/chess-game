using ChessGame.Business.Contracts.Models;
using ChessGame.Business.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessGame.Business.Services
{
    public class PlayerServiceWithLogging : IPlayerService
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<PlayerServiceWithLogging> _logger;

        public PlayerServiceWithLogging(IPlayerService playerService, ILogger<PlayerServiceWithLogging> logger)
        {
            _playerService = playerService;
            _logger = logger;
        }


        public async Task<IEnumerable<Player>> FindAsync(string searchTerm)
        {
            try
            {
                return await _playerService.FindAsync(searchTerm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error getting players");
                throw;
            }
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            try
            {
                return await _playerService.GetByIdAsync(playerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error getting the player by id");
                throw;
            }
        }

        public async Task CreateAsync(Player player)
        {
            try
            {
                await _playerService.CreateAsync(player);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error creating the player");
                throw;
            }
        }

        public async Task EditAsync(Player player)
        {
            try
            {
                await _playerService.EditAsync(player);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error editing the player");
                throw;
            }
        }

        public async Task DeleteAsync(int playerId)
        {
            try
            {
                await _playerService.DeleteAsync(playerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error deleting the player");
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _playerService.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error savign the changes");
                throw;
            }
        }

        public async Task<bool> PlayerHasGamesAsync(int playerId)
        {
            try
            {
                return await _playerService.PlayerHasGamesAsync(playerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error reading if player has game information");
                throw;
            }
        }
    }
}
