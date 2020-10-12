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
    public class PooledBookController : ControllerBase
    {
        private ILogger _logger { get; set; }
        private IBfConfiguration _configuration { get; set; }
        private IEntityRepository<PooledBook> _entityRepo { get; set; }
        private IEntitySearch<PooledBook> _searchRepo { get; set; }

        public PooledBookController(
            ILogger<PooledBookController> logger,
            IBfConfiguration configuration,
            IEntityRepository<PooledBook> entityRepo,
            IEntitySearch<PooledBook> searchRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _entityRepo = entityRepo;
            _searchRepo = searchRepo;
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
                var searchResults = _searchRepo.Search(
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
