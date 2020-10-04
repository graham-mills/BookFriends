using System.Collections.Generic;
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
        public int ListingsPerPage { get; set; }
    }
}
