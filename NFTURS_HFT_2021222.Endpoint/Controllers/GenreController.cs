using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NFTURS_HFT_2021222.Endpoint.Services;
using NFTURS_HFT_2021222.Logic;
using NFTURS_HFT_2021222.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFTURS_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        IGenreLogic genreLogic;
        IHubContext<SignalRHub> hub;

        public GenreController(IGenreLogic genreLogic, IHubContext<SignalRHub> hub)
        {
            this.genreLogic = genreLogic;
            this.hub = hub;
        }



        // GET: api/<GenreController>
        [HttpGet]
        public IEnumerable<Genre> ReadAll()
        {
            return genreLogic.ReadAll();
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public Genre Read(int id)
        {
            return genreLogic.Read(id);
        }

        // POST api/<GenreController>
        [HttpPut]
        public void Update([FromBody] Genre value)
        {
            genreLogic.Update(value);
            this.hub.Clients.All.SendAsync("GenreUpdated", value);
        }

        // PUT api/<GenreController>/5
        [HttpPost]
        public void Create([FromBody] Genre value)
        {
            genreLogic.Create(value);
            this.hub.Clients.All.SendAsync("GenreCreated", value);
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var GenreToDelete = this.genreLogic.Read(id);
            genreLogic.Delete(id);
            this.hub.Clients.All.SendAsync("GenreDeleted", GenreToDelete);
        }
    }
}
