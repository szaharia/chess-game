using ChessGame.Business.Contracts.Models;

namespace ChessGame.InternalClasses
{
    public static class PlayerExtensions
    {
        public static string GetFullName(this Player player)
        {
            return $"{player.FirstName} {player.LastName}";
        }
    }
}
