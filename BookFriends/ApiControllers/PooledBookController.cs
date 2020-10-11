using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookFriends.ApiControllers.Dtos;
using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookFriends.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PooledBookController : ControllerBase
    {
        private ILogger _logger { get; set; }
        private IBfConfiguration _configuration { get; set; }
        private IEntityRepository<PooledBook> _entityRepo { get; set; }

        public PooledBookController(
            ILogger<PooledBookController> logger,
            IBfConfiguration configuration,
            IEntityRepository<PooledBook> entityRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _entityRepo = entityRepo;
        }

        [HttpGet]
        public GetResult<PooledBookDto> Get(Guid community, string? q, int limit, int offset)
        {
            var getResult = new GetResult<PooledBookDto>();
            if (q.IsNullOrEmpty())
            {
                getResult.TotalRecords = _entityRepo.Count();
                getResult.Data = _entityRepo.Get(take: limit,
                                                skip: offset,
                                                filter: e => e.CommunityMember.CommunityGroup.Id.Equals(community))
                                           .Select(e => new PooledBookDto(e));
            }
            else
            {
                var entitySearch = new EntitySearch<PooledBook>(_entityRepo);
                var searchResults = entitySearch.Search(
                    searchQuery: q,
                    resultsToTake: limit,
                    resultsToSkip: offset,
                    entityFilter: e => e.CommunityMember.CommunityGroup.Id.Equals(community));

                getResult.Data = searchResults.MatchedEntities.Select(e => new PooledBookDto(e));
                getResult.TotalRecords = searchResults.TotalMatchedEntities;
            }

            return getResult;
        }
    }
}
