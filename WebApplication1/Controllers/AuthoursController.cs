using Contract.models;
using Contract.Resourse;
using Domain.mangers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AuthorPublisherProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoursController : ControllerBase
    {

        private readonly IAuthorMangers Authormanger;
        public AuthoursController(IAuthorMangers Authormanger)
        {
            this.Authormanger = Authormanger;

        }
        [HttpGet]
        public async Task<IEnumerable<AuthorResource>> GetAuthors()
        {
            return await Authormanger.GetAuthors();

        }
        [HttpGet("{id}")]
        public async Task<AuthorResource> GetAuthor(int id)
        {
            return await Authormanger.GetAuthor(id);
        }
        [HttpPost]
        public async Task<ActionResult<AuthorResource>> CreateAuthor([FromBody] AuthorModel newAuthorModel)
        {
            return await Authormanger.CreateAuthor(newAuthorModel);
        }
        [HttpPut("{Id}")]

        public async Task<AuthorResource> PutAuthor(int id, [FromBody] AuthorModel model)
        {
            return await Authormanger.PutAuthor(id, model);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await Authormanger.Delete(id);
        }
    }
}
