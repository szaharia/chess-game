using ChessGame.Data.Contracts;
using ChessGame.Data.Contracts.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessGame.Data.Repositories
{
    public class PlayerRepositoryWithLogging : IPlayerRepository
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<PlayerRepositoryWithLogging> _logger;

        public PlayerRepositoryWithLogging(IPlayerRepository playerRepository, ILogger<PlayerRepositoryWithLogging> logger)
        {
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Player>> FindAsync(string searchTerm)
        {
            try
            {
                _logger.LogInformation("Started finding a player");
                return await _playerRepository.FindAsync(searchTerm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error getting the players");
                throw;
            }
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            try
            {
                return await _playerRepository.GetByIdAsync(playerId);
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
                await _playerRepository.CreateAsync(player);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error creating a player");
                throw;
            }
        }

        public async Task EditAsync(Player player)
        {
            try
            {
                await _playerRepository.EditAsync(player);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error edditing a player");
                throw;
            }
        }

        public async Task DeleteAsync(Player player)
        {
            try
            {
                await _playerRepository.DeleteAsync(player);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error deleting a players");
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _playerRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error saving the changes");
                throw;
            }
        }

        public async Task<bool> PlayerHasGamesAsync(int playerId)
        {
            try
            {
                return await _playerRepository.PlayerHasGamesAsync(playerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error getting information about a player's games");
                throw;
            }
        }
    }
}
