using ChessGame.Data.Contracts.Entities;
using ChessGame.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessGame.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _dbContext;

        public PlayerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Player>> FindAsync(string searchTerm)
        {
            return string.IsNullOrWhiteSpace(searchTerm)
                ? await _dbContext.Players.ToListAsync()
                : await _dbContext.Players
                    .Where(p => EF.Functions.Like(p.LastName, $"%{searchTerm}%"))
                    .ToListAsync();
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            return await _dbContext.Players
                .SingleOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task CreateAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            await _dbContext.AddAsync(player);
        }

        public async Task EditAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            var repositoryPlayer = await GetByIdAsync(player.Id);
            if (repositoryPlayer == null)
            {
                throw new ArgumentException("Player not found in DB");
            }

            repositoryPlayer.FirstName = player.FirstName;
            repositoryPlayer.LastName = player.LastName;
            repositoryPlayer.Rating = player.Rating;
        }

        public async Task DeleteAsync(Player player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            var repositoryPlayer = await GetByIdAsync(player.Id);
            if (repositoryPlayer == null)
            {
                throw new ArgumentException("Player not found in DB");
            }

            _dbContext.Remove(player);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
