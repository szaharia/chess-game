using ChessGame.Data.Contracts.Entities;
using ChessGame.CrossCut;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGame.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Game>()
                .Property(e => e.Result)
                .HasConversion(
                    v => v.GetDisplayName(),
                    v => EnumHelpers.GetEnumByDisplayName<Winner>(v, Winner.Draw));

            //seed players
            modelBuilder.Entity<Player>().HasData(new Player { Id = 1, FirstName = "John", LastName = "Smith", Rating = 100 });
            modelBuilder.Entity<Player>().HasData(new Player { Id = 2, FirstName = "Joanne", LastName = "Doe", Rating = 200 });
            modelBuilder.Entity<Player>().HasData(new Player { Id = 3, FirstName = "Silviu", LastName = "Zaharia", Rating = 200 });

            //seed games
            modelBuilder.Entity<Game>().HasData(new Game { Id = 1, WhitePlayerId = 1, BlackPlayerId = 2, Date = DateTime.Now.AddDays(-10), OpeningClassification = "A10", Result = Winner.WhiteWins });
            modelBuilder.Entity<Game>().HasData(new Game { Id = 2, WhitePlayerId = 1, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-10), OpeningClassification = "B13", Result = Winner.WhiteWins });
            modelBuilder.Entity<Game>().HasData(new Game { Id = 3, WhitePlayerId = 2, BlackPlayerId = 3, Date = DateTime.Now.AddDays(-9), OpeningClassification = "E03", Result = Winner.Draw });
            modelBuilder.Entity<Game>().HasData(new Game { Id = 4, WhitePlayerId = 1, BlackPlayerId = 2, Date = DateTime.Now.AddDays(-9), OpeningClassification = "F17", Result = Winner.WhiteWins });
            modelBuilder.Entity<Game>().HasData(new Game { Id = 5, WhitePlayerId = 2, BlackPlayerId = 2, Date = DateTime.Now, OpeningClassification = "B17", Result = Winner.WhiteWins });
        }
    }
}
