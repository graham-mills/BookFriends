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
            Communities = new List<CommunityGroup>();
            Memberships = new List<CommunityMember>();
        }
        public List<CommunityGroup> Communities { get; set; }
        public List<CommunityMember> Memberships { get; set; }
    }
}
