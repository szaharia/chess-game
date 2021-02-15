using ChessGame.Business.Contracts.Models;
using ChessGame.Business.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessGame.Business.Services
{
    public class GameServiceWithLogging: IGameService
    {
        private readonly IGameService _gameService;
        private readonly ILogger<PlayerServiceWithLogging> _logger;

        public GameServiceWithLogging(IGameService gameService, ILogger<PlayerServiceWithLogging> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        public async Task<IEnumerable<Game>> FindAsync(string searchTerm)
        {
            try
            {
                return await _gameService.FindAsync(searchTerm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error getting games");
                throw;
            }
        }

        public async Task<Game> GetByIdAsync(int gameId)
        {
            try
            {
                return await _gameService.GetByIdAsync(gameId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error getting game by id");
                throw;
            }
        }
        public async Task CreateAsync(Game game)
        {
            try
            {
                await _gameService.CreateAsync(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error creating a new game");
                throw;
            }
        }

        public async Task DeleteAsync(int gameId)
        {
            try
            {
                await _gameService.DeleteAsync(gameId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error deleting a game");
                throw;
            }
        }

        public async Task EditAsync(Game game)
        {
            try
            {
                await _gameService.EditAsync(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error editing a game");
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _gameService.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error saving game changes");
                throw;
            }
        }
    }
}
