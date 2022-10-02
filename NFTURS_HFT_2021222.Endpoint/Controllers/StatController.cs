using Microsoft.AspNetCore.Mvc;
using NFTURS_HFT_2021222.Logic;
using NFTURS_HFT_2021222.Models;
using System.Collections.Generic;
using static NFTURS_HFT_2021222.Logic.GameLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFTURS_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGameLogic gameLogic;

        public StatController(IGameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        [HttpGet]
        public IEnumerable<GameInfo> BestRatedGameInfo()
        {
            
            IEnumerable<GameInfo> gf = (IEnumerable<GameInfo>)gameLogic.BestRatedGameInfo();
            return gf;
        }

        [HttpGet]
        public IEnumerable<GameInfo> WorstRatedGameInfo()
        {
            IEnumerable<GameInfo> gf = (IEnumerable<GameInfo>)gameLogic.WorstRatedGameInfo();
            return gf;
        }

        [HttpGet]
        public IEnumerable<GameInfo> MostReviewedGameInfo()
        {
            IEnumerable<GameInfo> gf = (IEnumerable<GameInfo>)gameLogic.MostReviewedGameInfo();
            return gf;
        }

        [HttpGet]
        public IEnumerable<GameInfo> AllSoulsLikeGamesInfo()
        {
            return gameLogic.AllSoulsLikeGamesInfo();
        }

        [HttpGet]
        public IEnumerable<GameInfo> MultiplayerInfo()
        {
            return gameLogic.MultiplayerInfo();
        }



    }
}
