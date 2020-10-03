using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookFriends.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityGroupController : ControllerBase
    {
        private readonly IEntityRepository<CommunityGroup> _entityRepo;

        public CommunityGroupController(IEntityRepository<CommunityGroup> entityRepo)
        {
            _entityRepo = entityRepo;
        }

        // GET: api/<CommunityGroupApiController>
        [HttpGet]
        public ActionResult<object[]> Get()
        {
            ICollection<object> dtos = new List<object>();
            _entityRepo.Get().ToList().ForEach(cg => dtos.Add(cg.ToAnonymousDto()));
            return dtos.ToArray();
        }

        // GET api/<CommunityGroupApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CommunityGroupApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CommunityGroupApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommunityGroupApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
