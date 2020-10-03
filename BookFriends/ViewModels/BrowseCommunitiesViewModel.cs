using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ViewModels
{
    public class BrowseCommunitiesViewModel
    {
        public BrowseCommunitiesViewModel()
        {
            CommunityGroupDtos = new List<object>();
            MembershipDtos = new List<object>();
        }
        public List<object> CommunityGroupDtos { get; set; }
        public List<object> MembershipDtos { get; set; }
        public int TotalCommunities { get; set; }
    }
}
