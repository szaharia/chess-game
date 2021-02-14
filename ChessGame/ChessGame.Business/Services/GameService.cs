using AutoMapper;
using ChessGame.Business.Contracts.Models;
using ChessGame.Business.Contracts.Services;
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
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
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
            await _gameRepository.EditAsync(_mapper.Map<Data.Contracts.Entities.Game>(game));
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _gameRepository.SaveChangesAsync();
        }
    }
}
