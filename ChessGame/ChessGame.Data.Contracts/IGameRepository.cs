using ChessGame.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Data.Contracts
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> FindAsync(string searchTerm);
        Task<Game> GetByIdAsync(int gameId);
        Task CreateAsync(Game game);
        Task EditAsync(Game game);
        Task DeleteAsync(Game game);
        Task<bool> SaveChangesAsync();
    }
}
