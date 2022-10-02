using Microsoft.AspNetCore.Mvc;
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

        public GameController(IGameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
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
            ;
            gameLogic.Update(value);
        }

        // PUT api/<GameController>/5
        [HttpPost]
        public void Create([FromBody] Game value)
        {
            gameLogic.Create(value);
        }


        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            gameLogic.Delete(id);
        }

       
    }
}
