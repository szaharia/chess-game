using System.Collections.Generic;
using System.Linq;
using ChessGame.Business.Contracts.Services;

namespace ChessGame.InternalClasses.Game
{
    public class NamedPlayersGetter : INamedPlayersGetter
    {
        private readonly IPlayerService _playerService;

        public NamedPlayersGetter(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public IEnumerable<NamedPlayer> Get()
        {
            var players = _playerService.FindAsync("").Result;

            return (from p in players
                select new NamedPlayer {Id = p.Id, FullName = p.GetFullName()});
        }
    }
}
