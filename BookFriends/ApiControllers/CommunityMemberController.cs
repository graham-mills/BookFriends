using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriends.ApiControllers.Dtos;
using BookFriendsDataAccess.Entities;
using BookFriendsDataAccess.Repository;
using BookFriendsDataAccess.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookFriends.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityMemberController : ControllerBase
    {
        private ILogger _logger { get; set; }
        private IBfConfiguration _configuration { get; set; }
        private IEntityRepository<CommunityMember> _entityRepo { get; set; }

        public CommunityMemberController(
            ILogger<CommunityMemberController> logger,
            IBfConfiguration configuration,
            IEntityRepository<CommunityMember> entityRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _entityRepo = entityRepo;
        }

        [HttpGet]
        public GetResult<CommunityMemberDto> Get(Guid community, int limit, int offset)
        {
            var getResult = new GetResult<CommunityMemberDto>();

            getResult.TotalRecords = _entityRepo.Count();
            getResult.Data = _entityRepo.Get(take: limit,
                                             skip: offset,
                                             filter: e => e.CommunityGroup.Id.Equals(community))
                                        .Select(e => new CommunityMemberDto(e));
            

            return getResult;
        }
    }
}
