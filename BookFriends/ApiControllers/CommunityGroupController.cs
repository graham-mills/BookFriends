using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriends.ApiControllers.Dtos;
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
        private ILogger _logger { get; set; }
        private IBfConfiguration _configuration { get; set; }
        private IEntityRepository<CommunityGroup> _entityRepo { get; set; }

        public CommunityGroupController(
            ILogger<CommunityGroupController> logger,
            IBfConfiguration configuration,
            IEntityRepository<CommunityGroup> entityRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _entityRepo = entityRepo;
        }

        [HttpGet]
        public GetResult<CommunityGroupDto> Get(string? q, int limit, int offset)
        {
            var getResult = new GetResult<CommunityGroupDto>();
            if (q.IsNullOrEmpty())
            {
                getResult.TotalRecords = _entityRepo.Count();
                getResult.Data = _entityRepo.Get(take: limit, skip: offset).Select(e => new CommunityGroupDto(e));
            }
            else
            {
                var entitySearch = new EntitySearch<CommunityGroup>(_entityRepo);
                var searchResults = entitySearch.Search(q, resultsToTake: limit, resultsToSkip: offset);
                getResult.Data = searchResults.MatchedEntities.Select(e => new CommunityGroupDto(e));
                getResult.TotalRecords = searchResults.TotalMatchedEntities;
            }

            return getResult;
        }
    }
}
