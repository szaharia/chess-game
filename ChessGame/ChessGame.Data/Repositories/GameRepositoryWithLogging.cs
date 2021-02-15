using ChessGame.Data.Contracts;
using ChessGame.Data.Contracts.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Data.Repositories
{
    public class GameRepositoryWithLogging: IGameRepository
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameRepositoryWithLogging> _logger;

        public GameRepositoryWithLogging(IGameRepository gameRepository, ILogger<GameRepositoryWithLogging> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Game>> FindAsync(string searchTerm)
        {
            try
            {
                return await _gameRepository.FindAsync(searchTerm);
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
                return await _gameRepository.GetByIdAsync(gameId);
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
                await _gameRepository.CreateAsync(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error creating a new game");
                throw;
            }
        }

        public async Task EditAsync(Game game)
        {
            try
            {
                await _gameRepository.EditAsync(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error editing a game");
                throw;
            }
        }

        public async Task DeleteAsync(Game game)
        {
            try
            {
                await _gameRepository.DeleteAsync(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error deleting a game");
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _gameRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error saving game changes");
                throw;
            }
        }
    }
}
