﻿using Contract.models;
using Contract.Resourse;
using Domain.mangers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorPublisherProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherManger publishermanger;
        public PublisherController(IPublisherManger publishermanger)
        {
            this.publishermanger = publishermanger;
        }
        [HttpGet]
        public async Task<IEnumerable<PublisherResource>> GetPublishers()
        {
            return await publishermanger.GetPublishers();
        }
        [HttpGet("{id}")]

        public async Task<PublisherResource> GetPublisher(int id)
        {
            return await publishermanger.GetPublisher(id);
        }
        [HttpPost]
        public async Task<ActionResult<PublisherResource>> CreatePublisherAsync([FromBody] PublisherModel newPublisherModel)
        {

            return await publishermanger.CreatePublisher(newPublisherModel);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PublisherResource>> PutPublisher(int id, [FromBody] PublisherModel model)
        {
            return await publishermanger.PutPublisher(id, model);
        }
        [HttpDelete("{Id}")]
        public async Task DeletePublisher(int Id)
        {
            await publishermanger.DeletePublisher(Id);
        }
    }
}
