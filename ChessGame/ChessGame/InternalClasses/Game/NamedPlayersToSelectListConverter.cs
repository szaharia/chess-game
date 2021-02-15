using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChessGame.InternalClasses.Game
{
    public class NamedPlayersToSelectListConverter : INamedPlayersToSelectListConverter
    {
        public SelectList Convert(IEnumerable<NamedPlayer> namedPlayers)
        {
            return new SelectList(namedPlayers, "Id", "FullName");
        }
        public SelectList Convert(IEnumerable<NamedPlayer> namedPlayers, int selectedPlayerId)
        {
            return new SelectList(namedPlayers, "Id", "FullName", selectedPlayerId);
        }
    }
}
