using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ViewModels
{
    public class BrowseCommunitiesViewModelBuilder
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IEntityRepository<CommunityGroup> _communityGroupRepo;

        public BrowseCommunitiesViewModel CommunityListings;

        public BrowseCommunitiesViewModelBuilder(ILogger logger, IConfiguration configuration, IEntityRepository<CommunityGroup> communityGroupRepo)
        {
            _logger = logger;
            _configuration = configuration;
            _communityGroupRepo = communityGroupRepo;
        }

        public void Build()
        {
            int communitiesToDisplay = _configuration.GetValue<int>(ConfigurationKeys.BrowseCommunitiesPaginationSize);

            CommunityListings = new BrowseCommunitiesViewModel();
            CommunityListings.Communities.AddRange( _communityGroupRepo.Get(take: communitiesToDisplay));
        }

    }
}
