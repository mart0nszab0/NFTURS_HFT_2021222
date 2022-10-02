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
    public class PublisherController : ControllerBase
    {
        IPublisherLogic publisherLogic;
        IHubContext<SignalRHub> hub;

        public PublisherController(IPublisherLogic publisherLogic, IHubContext<SignalRHub> hub)
        {
            
            this.publisherLogic = publisherLogic;
            this.hub = hub;
        }



        // GET: api/<PublisherController>
        [HttpGet]
        public IEnumerable<Publisher> ReadAll()
        {
            return publisherLogic.ReadAll();
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public Publisher Read(int id)
        {
            return publisherLogic.Read(id);
        }

        // POST api/<PublisherController>
        [HttpPut]
        public void Update([FromBody] Publisher value)
        {
            publisherLogic.Update(value);
            this.hub.Clients.All.SendAsync("PublisherUpdated", value);
        }

        // PUT api/<PublisherController>/5
        [HttpPost]
        public void Create([FromBody] Publisher value)
        {
            publisherLogic.Create(value);
            this.hub.Clients.All.SendAsync("PublisherCreated", value);
        }

        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var PublisherToDelete = this.publisherLogic.Read(id);
            publisherLogic.Delete(id);
            this.hub.Clients.All.SendAsync("PublisherDeleted", PublisherToDelete);
        }
    }
}