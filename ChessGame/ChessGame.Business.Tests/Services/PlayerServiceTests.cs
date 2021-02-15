using System.Collections.Generic;
using AutoMapper;
using ChessGame.Business.Services;
using ChessGame.Data.Contracts;
using Moq;
using NUnit.Framework;
using Player = ChessGame.Data.Contracts.Entities.Player;

namespace ChessGame.Business.Tests.Services
{
    [TestFixture]
    public class PlayerServiceTests
    {
        [Test]
        public void FindAsync_WhenSearchReturnsNoResults_JustPlayerRepositoryIsHit()
        {
            // Arrange
            List<Player> players = null;

            var mockPlayerRepository = new Mock<IPlayerRepository>();
            mockPlayerRepository
                .Setup(x => x.FindAsync(It.IsAny<string>()))
                .ReturnsAsync(players);

            var mockMapper = new Mock<IMapper>( );
            mockMapper
                .Setup(x => x.Map<List<Player>>(It.IsAny<List<Contracts.Models.Player>>()))
                .Returns(() => new List<Player>());


            PlayerService playerService = new PlayerService(mockPlayerRepository.Object, mockMapper.Object);
            
            // Act
            var response = playerService.FindAsync(null).Result;

            // Assert
            mockPlayerRepository.Verify(x => x.FindAsync(It.IsAny<string>()), Times.Exactly(1));
            mockMapper.Verify(x => x.Map<It.IsAnyType>(null), Times.Exactly(0));
        }

        // TODO: Add full list of test
    }
}
