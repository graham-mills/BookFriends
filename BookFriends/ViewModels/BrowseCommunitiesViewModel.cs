using BookFriends.ApiControllers.Dtos;
using BookFriendsDataAccess.Entities;
using System.Collections.Generic;
namespace BookFriends.ViewModels
{
    public class BrowseCommunitiesViewModel
    {
        public BrowseCommunitiesViewModel()
        {
            CommunityGroups = new List<CommunityGroupDto>();
        }
        public IEnumerable<CommunityGroupDto> CommunityGroups { get; set; }
        public int TotalCommunityGroups { get; set; }
        public int ListingsPerPage { get; set; }
    }
}
