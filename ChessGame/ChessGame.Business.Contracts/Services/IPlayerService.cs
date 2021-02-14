using ChessGame.Business.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessGame.Business.Contracts.Services
{
    public interface IPlayerService
    {
        public Task<IEnumerable<Player>> FindAsync(string searchTerm);
        public Task<Player> GetByIdAsync(int playerId);
        public Task CreateAsync(Player player);
        public Task EditAsync(Player player);
        public Task DeleteAsync(int playerId);
        public Task<bool> SaveChangesAsync();
    }
}
