using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookFriends.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityGroupController : ControllerBase
    {
        private readonly ILogger<CommunityGroupController> _logger;
        private readonly IEntityRepository<CommunityGroup> _entityRepo;
        private readonly IBfConfiguration _configuration;

        public CommunityGroupController(ILogger<CommunityGroupController> logger, IBfConfiguration configuration, IEntityRepository<CommunityGroup> entityRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _entityRepo = entityRepo;
        }

        // GET: api/<CommunityGroupApiController>
        [HttpGet]
        public ActionResult<object[]> Get(int items = 0, int page = 1)
        {
            if (page < 1) return BadRequest(page);


            ICollection<object> dtos = new List<object>();
            int itemsToSkip = (page - 1) * items;
            _entityRepo.Get(take: items, skip: itemsToSkip).ToList().ForEach(cg => dtos.Add(cg.ToAnonymousDto()));
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
