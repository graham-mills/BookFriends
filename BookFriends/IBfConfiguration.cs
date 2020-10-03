namespace BookFriends
{
    public interface IBfConfiguration
    {
        int BrowseCommunitiesListingsPerPage { get; }
        int BrowseCommunitiesMembershipsPerPage { get; }
        int ViewCommunityBooksPerPage { get; }
        int ViewCommunityMembersPerPage { get; }
    }
}