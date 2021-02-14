using ChessGame.Business.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Business.Contracts.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> FindAsync(string searchTerm);
        Task<Game> GetByIdAsync(int gameId);
        Task CreateAsync(Game game);
        Task EditAsync(Game game);
        Task DeleteAsync(int gameId);
        Task<bool> SaveChangesAsync();
    }
}
