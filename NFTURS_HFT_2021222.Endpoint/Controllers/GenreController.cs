using Microsoft.AspNetCore.Mvc;
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

        public GenreController(IGenreLogic genreLogic)
        {
            this.genreLogic = genreLogic;
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
        }

        // PUT api/<GenreController>/5
        [HttpPost]
        public void Create([FromBody] Genre value)
        {
            genreLogic.Create(value);
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            genreLogic.Delete(id);
        }
    }
}
