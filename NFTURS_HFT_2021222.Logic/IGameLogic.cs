using NFTURS_HFT_2021222.Models;
using System.Collections.Generic;

namespace NFTURS_HFT_2021222.Logic
{
    public interface IGameLogic
    {
        IEnumerable<GameInfo> AllSoulsLikeGamesInfo();
        IEnumerable<GameInfo> BestRatedGameInfo();
        void Create(Game game);
        void Delete(int id);
        IEnumerable<GameInfo> MostReviewedGameInfo();
        IEnumerable<GameInfo> MultiplayerInfo();
        Game Read(int id);
        IEnumerable<Game> ReadAll();
        void Update(Game item);
        IEnumerable<GameInfo> WorstRatedGameInfo();
    }
}