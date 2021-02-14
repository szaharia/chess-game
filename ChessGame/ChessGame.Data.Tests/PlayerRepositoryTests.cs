using ChessGame.Data.Contracts.Entities;
using ChessGame.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace ChessGame.Data.Tests
{
    public class PlayerRepositoryTests
    {
        [Test]
        public void GetAllAsync_SearchTermIsNull_ReturnAllPlayers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Players.Add(new Player { Id = 1, FirstName = "John", LastName = "Smith", Rating = 100 });
                context.Players.Add(new Player { Id = 2, FirstName = "Joanne", LastName = "Doe", Rating = 200 });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            { 
                var playerRepository = new PlayerRepository(context);

                // Act
                var players = playerRepository.FindAsync(null).Result;
                
                // Assert
                Assert.AreEqual(2, players.Count());
            }
        }

        [Test]
        public void GetAllAsync_SearchTermIsEmpty_ReturnAllPlayers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Players.Add(new Player { Id = 1, FirstName = "John", LastName = "Smith", Rating = 100 });
                context.Players.Add(new Player { Id = 2, FirstName = "Joanne", LastName = "Doe", Rating = 200 });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var playerRepository = new PlayerRepository(context);

                // Act
                var players = playerRepository.FindAsync(string.Empty).Result;

                // Assert
                Assert.AreEqual(2, players.Count());
            }
        }

        [Test]
        public void GetAllAsync_SearchTermIsNotAMatch_ReturnNoPlayers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Players.Add(new Player { Id = 1, FirstName = "John", LastName = "Smith", Rating = 100 });
                context.Players.Add(new Player { Id = 2, FirstName = "Joanne", LastName = "Doe", Rating = 200 });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var playerRepository = new PlayerRepository(context);

                // Act
                var players = playerRepository.FindAsync("Gates").Result;

                // Assert
                Assert.AreEqual(0, players.Count());
            }
        }

        [Test]
        public void GetAllAsync_SearchTermIsAMatch_ReturnFilteredPlayers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"ChessGameForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Players.Add(new Player { Id = 1, FirstName = "John", LastName = "Smith", Rating = 100 });
                context.Players.Add(new Player { Id = 2, FirstName = "Joanne", LastName = "Doe", Rating = 200 });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var playerRepository = new PlayerRepository(context);

                // Act
                var players = playerRepository.FindAsync("Smith").Result.ToList();

                // Assert
                Assert.AreEqual(1, players.Count());
                Assert.AreEqual(1, players[0].Id);
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
                context.Players.Add(new Player { Id = 1, FirstName = "John", LastName = "Smith", Rating = 100 });
                context.Players.Add(new Player { Id = 2, FirstName = "Joanne", LastName = "Doe", Rating = 200 });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var playerRepository = new PlayerRepository(context);

                // Act
                // Assert
                Assert.ThrowsAsync<ArgumentNullException>(() => playerRepository.EditAsync(null));
                Assert.ThrowsAsync<ArgumentException>(() => playerRepository.EditAsync(new Player()));
                Assert.ThrowsAsync<ArgumentException>(() => playerRepository.EditAsync(new Player { Id = 10 }));
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
                context.Players.Add(new Player { Id = 1, FirstName = "John", LastName = "Smith", Rating = 100 });
                context.Players.Add(new Player { Id = 2, FirstName = "Joanne", LastName = "Doe", Rating = 200 });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                var playerRepository = new PlayerRepository(context);

                // Act
                // Assert
                Assert.ThrowsAsync<ArgumentNullException>(() => playerRepository.DeleteAsync(null));
                Assert.ThrowsAsync<ArgumentException>(() => playerRepository.DeleteAsync(new Player()));
                Assert.ThrowsAsync<ArgumentException>(() => playerRepository.DeleteAsync(new Player { Id = 10 }));
            }
        }
    }
}