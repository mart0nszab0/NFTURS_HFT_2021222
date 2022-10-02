using Microsoft.AspNetCore.Mvc;
using NFTURS_HFT_2021222.Logic;
using NFTURS_HFT_2021222.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NFTURS_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        IPublisherLogic publisherLogic;

        public PublisherController(IPublisherLogic publisherLogic)
        {
            
            this.publisherLogic = publisherLogic;
        }



        // GET: api/<GameController>
        [HttpGet]
        public IEnumerable<Publisher> ReadAll()
        {
            return publisherLogic.ReadAll();
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public Publisher Read(int id)
        {
            return publisherLogic.Read(id);
        }

        // POST api/<GameController>
        [HttpPut]
        public void Update([FromBody] Publisher value)
        {
            publisherLogic.Update(value);
        }

        // PUT api/<GameController>/5
        [HttpPost]
        public void Create([FromBody] Publisher value)
        {
            publisherLogic.Create(value);
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            publisherLogic.Delete(id);
        }
    }
}