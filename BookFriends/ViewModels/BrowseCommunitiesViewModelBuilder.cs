using BookFriends.Data;
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
        private readonly BookFriendsDbContext _context;

        public BrowseCommunitiesViewModel CommunityListings;

        public BrowseCommunitiesViewModelBuilder(ILogger logger, IConfiguration configuration, BookFriendsDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public void Build()
        {
            int communitiesToDisplay = _configuration.GetValue<int>(ConfigurationKeys.BrowseCommunitiesPaginationSize);

            CommunityListings = new BrowseCommunitiesViewModel();
            CommunityListings.Communities.AddRange(
                    _context.CommunityGroups.Take(communitiesToDisplay).ToList()
                );
        }

    }
}
