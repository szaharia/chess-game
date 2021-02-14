using ChessGame.Data.Contracts.Entities;
using ChessGame.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Data.Tests
{
    public class GameRepositoryTests
    {
        [Test]
        public void GetAllAsync_SearchTermIsNullOrWhiteSpace_ArgumentExceptinIsThrown()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Games.Add(new Game { Id = 1, WhitePlayerId = 1, BlackPlayerId = 2, Date = DateTime.Now.AddDays(-10), OpeningClassification = "A10", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 2, WhitePlayerId = 1, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-10), OpeningClassification = "B13", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 3, WhitePlayerId = 2, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-9), OpeningClassification = "E03", Result = Winner.Draw });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var gameRepository = new GameRepository(context);

                // Act
                // Assert
                Assert.ThrowsAsync<ArgumentException>(() => gameRepository.FindAsync(null));
                Assert.ThrowsAsync<ArgumentException>(() => gameRepository.FindAsync(string.Empty));
                Assert.ThrowsAsync<ArgumentException>(() => gameRepository.FindAsync("  "));
            }
        }

        [Test]
        public void GetAllAsync_SearchTermIsValid_GamesAreFiltered()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Games.Add(new Game { Id = 1, WhitePlayerId = 1, BlackPlayerId = 2, Date = DateTime.Now.AddDays(-10), OpeningClassification = "A10", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 2, WhitePlayerId = 1, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-10), OpeningClassification = "B13", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 3, WhitePlayerId = 2, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-9), OpeningClassification = "E03", Result = Winner.Draw });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var gameRepository = new GameRepository(context);

                // Act
                var gamesByOpeningClassification = gameRepository.FindAsync("A10").Result.ToList();

                // Assert
                Assert.AreEqual(1, gamesByOpeningClassification.Count());
                Assert.AreEqual(1, gamesByOpeningClassification[0].Id);
            }
        }

        [Test]
        public void EditAsync_InexistingGamePassed_ArgumentExceptionIsThrown()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Games.Add(new Game { Id = 1, WhitePlayerId = 1, BlackPlayerId = 2, Date = DateTime.Now.AddDays(-10), OpeningClassification = "A10", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 2, WhitePlayerId = 1, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-10), OpeningClassification = "B13", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 3, WhitePlayerId = 2, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-9), OpeningClassification = "E03", Result = Winner.Draw });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var gameRepository = new GameRepository(context);

                // Act
                // Assert
                Assert.ThrowsAsync<ArgumentNullException>(() => gameRepository.EditAsync(null));
                Assert.ThrowsAsync<ArgumentException>(() => gameRepository.EditAsync(new Game()));
                Assert.ThrowsAsync<ArgumentException>(() => gameRepository.EditAsync(new Game { Id = 10 }));
            }
        }

        [Test]
        public void DeleteAsync_InexistingGamePassed_ArgumentExceptionIsThrown()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Games.Add(new Game { Id = 1, WhitePlayerId = 1, BlackPlayerId = 2, Date = DateTime.Now.AddDays(-10), OpeningClassification = "A10", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 2, WhitePlayerId = 1, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-10), OpeningClassification = "B13", Result = Winner.WhiteWins });
                context.Games.Add(new Game { Id = 3, WhitePlayerId = 2, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-9), OpeningClassification = "E03", Result = Winner.Draw });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var gameRepository = new GameRepository(context);

                // Act
                // Assert
                Assert.ThrowsAsync<ArgumentNullException>(() => gameRepository.DeleteAsync(null));
                Assert.ThrowsAsync<ArgumentException>(() => gameRepository.DeleteAsync(new Game()));
                Assert.ThrowsAsync<ArgumentException>(() => gameRepository.DeleteAsync(new Game { Id = 10 }));
            }
        }
    }
}
