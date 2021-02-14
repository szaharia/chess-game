using ChessGame.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Data.Contracts
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> FindAsync(string searchTerm);
        Task<Player> GetByIdAsync(int playerId);
        Task CreateAsync(Player player);
        Task EditAsync(Player player);
        Task DeleteAsync(Player player);
        Task<bool> SaveChangesAsync();
    }

}
