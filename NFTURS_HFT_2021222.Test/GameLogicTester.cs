using Moq;
using NFTURS_HFT_2021222.Logic;
using NFTURS_HFT_2021222.Models;
using NFTURS_HFT_2021222.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NFTURS_HFT_2021222.Logic.GameLogic;

namespace NFTURS_HFT_2021222.Test
{
    [TestFixture]
    public class GameLogicTester
    {
        //game
        IGameLogic gameLogic;
        Mock<IRepository<Game>> mockGameRepo;

        //genre
        IGenreLogic genreLogic;
        Mock<IRepository<Genre>> mockGenreRepo;

        //publisher
        IPublisherLogic publisherLogic;
        Mock<IRepository<Publisher>> mockPublisherRepo;

        [SetUp]
        public void Init()
        {
            //game setup
            var gameTestData = new List<Game>()
            {
                new Game(){GameId=1, Name="Mario", PublisherID=4, GenreID=3, NumberOfReviews=10,
                GameRating=4.7, Mode=Game.GameMode.Singleplayer},

                new Game(){GameId=2, Name="AC Origins", PublisherID=3, GenreID=2 , NumberOfReviews=4,
                GameRating=4.1, Mode=Game.GameMode.Singleplayer},

                new Game(){GameId=3, Name="World Of Warcraft", PublisherID=2, GenreID=1 , NumberOfReviews=39,
                GameRating=3.2, Mode=Game.GameMode.Multiplayer},

                new Game(){GameId=4, Name="XCOM", PublisherID=2, GenreID=4, NumberOfReviews=2,
                GameRating=3.8, Mode=Game.GameMode.Singleplayer},

                new Game(){GameId=5, Name="GTA San Andreas", PublisherID=6, GenreID=9, NumberOfReviews=343,
                GameRating=4.1, Mode=Game.GameMode.Singleplayer},

                new Game(){GameId=6, Name="GTA V", PublisherID=6, GenreID=9, NumberOfReviews=501,
                GameRating=4.8, Mode=Game.GameMode.Singleplayer},

                new Game(){GameId=7, Name="Dark Souls", PublisherID=11, GenreID=7, NumberOfReviews=176,
                GameRating=4.2, Mode=Game.GameMode.Singleplayer},

                new Game(){GameId=8, Name="Bloodborne", PublisherID=10, GenreID=7, NumberOfReviews=92,
                GameRating=0, Mode=Game.GameMode.Singleplayer},

                new Game(){GameId=9, Name="GTA Online", PublisherID=6, GenreID=9, NumberOfReviews=1972,
                GameRating=2.3, Mode=Game.GameMode.Multiplayer}
            }.AsQueryable();

            mockGameRepo = new Mock<IRepository<Game>>();
            mockGameRepo.Setup(m => m.ReadAll()).Returns(gameTestData);
            gameLogic = new GameLogic(mockGameRepo.Object);


            //genre setup
            mockGenreRepo = new Mock<IRepository<Genre>>();
            genreLogic = new GenreLogic(mockGenreRepo.Object);

            //publisher setup
            mockPublisherRepo = new Mock<IRepository<Publisher>>();
            publisherLogic = new PublisherLogic(mockPublisherRepo.Object);
        }

        //1: Create test 1
        [Test]
        public void GameCreateExceptionTest()
        {
            Game game = new Game() { Name = "" };
            Assert.Throws<ArgumentException>(() => gameLogic.Create(game));
        }

        //2: Create test 2
        [Test]
        public void GenreCreateExceptionTest()
        {
            Genre genre = new Genre() { Name = "" };
            Assert.Throws<ArgumentException>(() => genreLogic.Create(genre));
        }

        //3: Create test 3
        [Test]
        public void PublisherCreateExceptionTest()
        {
            Publisher publisher = new Publisher() { Name = "" };
            Assert.Throws<ArgumentException>(() => publisherLogic.Create(publisher));
        }

        //4: Non-crud test 1
        [Test]
        public void BestRatedGameInfoTest()
        {
            
            List<GameInfo> best = (List<GameInfo>)gameLogic.BestRatedGameInfo();
            Assert.That(best[0].Name, Is.EqualTo("GTA V"));

        }

        //5: Non-crud test 2
        [Test]
        public void WorstRatedGameInfoTest()
        {
            List<GameInfo> worst = (List<GameInfo>)gameLogic.WorstRatedGameInfo();
            Assert.That(worst[0].Name, Is.EqualTo("Bloodborne"));

        }

        //6: Non-crud test 3
        [Test]
        public void MostReviewedGameInfoTest()
        {
            List<GameInfo> mostReviewed = (List<GameInfo>)gameLogic.MostReviewedGameInfo();
            Assert.That(mostReviewed[0].Name, Is.EqualTo("GTA Online"));
        }

        //7: Non-crud test 4
        [Test]
        public void AllSoulsLikeGamesInfoTest()
        {
            var gameInfo = gameLogic
                .AllSoulsLikeGamesInfo()
                .Select(g => g.Name);

            int numOfGames = gameInfo.Count();
            Assert.That(gameInfo.Contains("Dark Souls") && gameInfo.Contains("Bloodborne") && numOfGames == 2);
        }

        //8: Non-crud test 5
        [Test]
        public void MultiplayerInfoTest()
        {
            var gameInfo = gameLogic
                .MultiplayerInfo()
                .Select(g => g.Name);

            int numOfGames = gameInfo.Count();
            Assert.That(gameInfo.Contains("GTA Online") && gameInfo.Contains("World Of Warcraft") && numOfGames == 2);
        }

        //9: Optional test 1
        [Test]
        public void GameReadTest()
        {
            Game game = gameLogic.Read(1);
            mockGameRepo.Verify(m => m.Read(1), Times.Once);
        }

        //10: Optional test 2
        [Test]
        public void GameDeleteTest()
        {
            gameLogic.Delete(1);
            mockGameRepo.Verify(m => m.Delete(1), Times.Once);
        }
    }


}
