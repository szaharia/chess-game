using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChessGame.InternalClasses.Game
{
    public interface INamedPlayersToSelectListConverter
    {
        SelectList Convert(IEnumerable<NamedPlayer> namedPlayers);
        SelectList Convert(IEnumerable<NamedPlayer> namedPlayers, int selectedPlayerId);
    }
}