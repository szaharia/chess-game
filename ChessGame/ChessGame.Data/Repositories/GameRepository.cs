using ChessGame.Data.Contracts;
using ChessGame.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _dbContext;

        public GameRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Game>> FindAsync(string searchTerm)
        {
            return string.IsNullOrWhiteSpace(searchTerm)
                ? await _dbContext.Games.ToListAsync()
                : await _dbContext.Games
                    .Where(p => EF.Functions.Like(p.OpeningClassification, $"%{searchTerm}%"))
                    .ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int gameId)
        {
            return await _dbContext.Games.SingleOrDefaultAsync(g => g.Id == gameId);
        }

        public async Task CreateAsync(Game game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            await _dbContext.AddAsync(game);
        }

        public async Task EditAsync(Game game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            var repositoryGame = await GetByIdAsync(game.Id);
            if (repositoryGame == null)
            {
                throw new ArgumentException("Game not found in DB");
            }

            repositoryGame.WhitePlayerId = game.WhitePlayerId;
            repositoryGame.BlackPlayerId= game.BlackPlayerId;
            repositoryGame.Date = game.Date;
            repositoryGame.OpeningClassification = game.OpeningClassification;
            repositoryGame.Result = game.Result;
        }

        public async Task DeleteAsync(Game game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            var repositoryGame = await GetByIdAsync(game.Id);
            if (repositoryGame == null)
            {
                throw new ArgumentException("Game not found in DB");
            }

            _dbContext.Remove(game);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
