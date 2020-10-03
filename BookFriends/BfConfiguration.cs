using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends
{
    /// <summary>
    /// Configuration adapter class to hide magic key strings
    /// </summary>
    public class BfConfiguration : IBfConfiguration
    {
        private IConfiguration _configuration;
        public BfConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int BrowseCommunitiesMembershipsPerPage { get => _configuration.GetValue<int>("BrowseCommunities_MembershipsPaginationSize"); }
        public int BrowseCommunitiesListingsPerPage { get => _configuration.GetValue<int>("BrowseCommunities_CommunitiesPaginationSize"); }
        public int ViewCommunityBooksPerPage { get => _configuration.GetValue<int>("ViewCommunity_BooksPaginationSize"); }
        public int ViewCommunityMembersPerPage { get => _configuration.GetValue<int>("ViewCommunity_MembersPaginationSize"); }
    }
}
