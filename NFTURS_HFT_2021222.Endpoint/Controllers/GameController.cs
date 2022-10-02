using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using NFTURS_HFT_2021222.Endpoint.Services;
using NFTURS_HFT_2021222.Logic;
using NFTURS_HFT_2021222.Models;
using System.Collections.Generic;
using static NFTURS_HFT_2021222.Logic.GameLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFTURS_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameLogic gameLogic;
        IHubContext<SignalRHub> hub;

        public GameController(IGameLogic gameLogic, IHubContext<SignalRHub> hub)
        {
            this.gameLogic = gameLogic;
            this.hub = hub;
        }



        // GET: api/<GameController>
        [HttpGet]
        public IEnumerable<Game> ReadAll()
        {
            return gameLogic.ReadAll();
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public Game Read(int id)
        {
            return gameLogic.Read(id);
        }

        // POST api/<GameController>
        [HttpPut]
        public void Update([FromBody] Game value)
        {
            gameLogic.Update(value);
            this.hub.Clients.All.SendAsync("GameUpdated", value);
        }

        // PUT api/<GameController>/5
        [HttpPost]
        public void Create([FromBody] Game value)
        {
            gameLogic.Create(value);
            this.hub.Clients.All.SendAsync("GameCreated", value);
        }


        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gameToDelete = this.gameLogic.Read(id);
            gameLogic.Delete(id);
            this.hub.Clients.All.SendAsync("GameDeleted", gameToDelete);
        }

       
    }
}
