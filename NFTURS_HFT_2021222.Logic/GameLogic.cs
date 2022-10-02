using NFTURS_HFT_2021222.Models;
using NFTURS_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFTURS_HFT_2021222.Logic
{
    public partial class GameLogic : IGameLogic
    {
        IRepository<Game> repo;

        public GameLogic(IRepository<Game> repo)
        {
            this.repo = repo;
        }

        public void Create(Game game)
        {
            if (game.Name.Length > 0)
            {
                repo.Create(game);
            }
            else
            {
                throw new ArgumentException("Invalid game name");
            }
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Game Read(int id)
        {
            return repo.Read(id);
        }

        public IEnumerable<Game> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Game item)
        {
            ;
            repo.Update(item);
        }

        //Non-Crud 1
        public IEnumerable<GameInfo> BestRatedGameInfo()
        {
            
            
            GameInfo item = repo
                .ReadAll()
                .Select(g => new GameInfo
                {
                    Name = g.Name,
                    Publisher = g.Publisher == null ? "" : g.Publisher.Name,
                    Genre = g.Genre == null ? "" : g.Genre.Name,
                    Rating = g.GameRating,
                    NumberOfReviews = g.NumberOfReviews
                })
                .OrderByDescending(g => g.Rating)
                .FirstOrDefault();

            IEnumerable<GameInfo> gf = new List<GameInfo>()
            {
                item
            };

            return gf;
        }

        //Non-Crud 2
        public IEnumerable<GameInfo> WorstRatedGameInfo()
        {
            GameInfo gf = repo
                .ReadAll()
                .Select(g => new GameInfo
                {
                    Name = g.Name,
                    Publisher = g.Publisher == null ? "" : g.Publisher.Name,
                    Genre = g.Genre == null ? "" : g.Genre.Name,
                    Rating = g.GameRating,
                    NumberOfReviews = g.NumberOfReviews
                })
                .OrderBy(g => g.Rating)
                .FirstOrDefault();

            IEnumerable<GameInfo> list = new List<GameInfo>()
            {
                gf
            };

            return list;
        }

        //Non-Crud 3
        public IEnumerable<GameInfo> MostReviewedGameInfo()
        {
            GameInfo gf = repo
                .ReadAll()
                .Select(g => new GameInfo
                {
                    Name = g.Name,
                    Publisher = g.Publisher == null ? "" : g.Publisher.Name,
                    Genre = g.Genre == null ? "" : g.Genre.Name,
                    Rating = g.GameRating,
                    NumberOfReviews = g.NumberOfReviews
                })
                .OrderByDescending(g => g.NumberOfReviews)
                .FirstOrDefault();

            IEnumerable<GameInfo> list = new List<GameInfo>()
            {
                gf
            };

            return list;
        }

        //Non-Crud 4
        public IEnumerable<GameInfo> AllSoulsLikeGamesInfo()
        {
            return repo
                .ReadAll()
                .Where(g => g.GenreID == 7)
                .Select(g => new GameInfo
                {
                    Name = g.Name,
                    Publisher = g.Publisher == null ? "" : g.Publisher.Name,
                    Genre = g.Genre == null ? "" : g.Genre.Name,
                    Rating = g.GameRating,
                    NumberOfReviews = g.NumberOfReviews
                });

        }

        //Non-Crud 5
        public IEnumerable<GameInfo> MultiplayerInfo()
        {
            return repo
                .ReadAll()
                .Where(g => g.Mode == Game.GameMode.Multiplayer)
                .Select(g => new GameInfo
                {
                    Name = g.Name,
                    Publisher = g.Publisher == null ? "" : g.Publisher.Name,
                    Genre = g.Genre == null ? "" : g.Genre.Name,
                    Rating = g.GameRating,
                    NumberOfReviews = g.NumberOfReviews
                });
        }
    }
}
