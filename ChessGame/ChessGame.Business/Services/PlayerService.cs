using AutoMapper;
using ChessGame.Business.Contracts.Models;
using ChessGame.Business.Contracts.Services;
using ChessGame.Business.InternalClasses;
using ChessGame.Data.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessGame.Business.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Player>> FindAsync(string searchTerm)
        {
            var players = await _playerRepository.FindAsync(searchTerm);

            if (players == null)
            {
                return new List<Player>(); ;
            }

            return _mapper.Map<List<Player>>(players);
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            
            if (player == null)
                return null;

            return _mapper.Map<Player>(player);
        }

        public async Task CreateAsync(Player player)
        {
            await _playerRepository.CreateAsync(_mapper.Map<Data.Contracts.Entities.Player>(player));
        }

        public async Task EditAsync(Player player)
        {
            await _playerRepository.EditAsync(_mapper.Map<Data.Contracts.Entities.Player>(player));
        }

        public async Task DeleteAsync(int playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
            {
                return;
            }

            if (await _playerRepository.PlayerHasGamesAsync(playerId))
            {
                throw new UndeleteablePlayerException("Cannot delete player, since it has existing games");
            }

            await _playerRepository.DeleteAsync(player);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _playerRepository.SaveChangesAsync();
        }

        public async Task<bool> PlayerHasGamesAsync(int playerId)
        {
            return await _playerRepository.PlayerHasGamesAsync(playerId);
        }
    }
}
