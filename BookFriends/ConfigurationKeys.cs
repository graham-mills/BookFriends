using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends
{
    public class ConfigurationKeys
    {
        // Communities/Browse
        public static readonly string BrowseCommunitiesPaginationSize = "BrowseCommunities_CommunitiesPaginationSize";
        public static readonly string BrowseMembershipsPaginationSize = "BrowseCommunities_MembershipsPaginationSize";

        // Community/View
        public static readonly string ViewCommunityMembersPaginationSize = "ViewCommunity_MembersPaginationSize";
        public static readonly string ViewCommunityBooksPaginationSize = "ViewCommunity_BooksPaginationSize";
    }
}
