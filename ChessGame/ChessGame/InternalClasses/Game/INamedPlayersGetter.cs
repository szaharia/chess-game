using System.Collections.Generic;

namespace ChessGame.InternalClasses.Game
{
    public interface INamedPlayersGetter
    {
        IEnumerable<NamedPlayer> Get();
    }
}