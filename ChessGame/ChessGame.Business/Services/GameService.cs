using AutoMapper;
using ChessGame.Business.Contracts.Models;
using ChessGame.Business.Contracts.Services;
using ChessGame.Business.InternalClasses;
using ChessGame.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Game>> FindAsync(string searchTerm)
        {
            var games = await _gameRepository.FindAsync(searchTerm);

            if (games == null)
            {
                return new List<Game>(); ;
            }

            return _mapper.Map<List<Game>>(games);
        }

        public async Task<Game> GetByIdAsync(int gameId)
        {
            var game = await _gameRepository.GetByIdAsync(gameId);

            if (game == null)
                return null;

            return _mapper.Map<Game>(game);
        }
        public async Task CreateAsync(Game game)
        {
            var whitePlayer = await _playerRepository.GetByIdAsync(game.WhitePlayerId);
            if (whitePlayer == null)
                throw new PlayerNotFoundException("Invalid White Player id");

            var blackPlayer = await _playerRepository.GetByIdAsync(game.BlackPlayerId);
            if (blackPlayer == null)
                throw new PlayerNotFoundException("Invalid Black Player id");

            await _gameRepository.CreateAsync(_mapper.Map<Data.Contracts.Entities.Game>(game));
        }

        public async Task DeleteAsync(int gameId)
        {
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game == null)
            {
                return;
            }

            await _gameRepository.DeleteAsync(game);
        }

        public async Task EditAsync(Game game)
        {
            var whitePlayer = await _playerRepository.GetByIdAsync(game.WhitePlayerId);
            if (whitePlayer == null)
                throw new PlayerNotFoundException("Invalid White Player id");

            var blackPlayer = await _playerRepository.GetByIdAsync(game.BlackPlayerId);
            if (blackPlayer == null)
                throw new PlayerNotFoundException("Invalid Black Player id");

            await _gameRepository.EditAsync(_mapper.Map<Data.Contracts.Entities.Game>(game));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _gameRepository.SaveChangesAsync();
        }
    }
}
