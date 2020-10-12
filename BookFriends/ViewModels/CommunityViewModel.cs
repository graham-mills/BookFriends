using BookFriends.ApiControllers.Dtos;
using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ViewModels
{
    public class CommunityViewModel
    {
        public CommunityGroupDto CommunityGroup { get; set; }
        public IEnumerable<PooledBookDto> PooledBooks { get; set; }
        public IEnumerable<CommunityMemberDto> Members { get; set; }
        public int TotalPooledBooks { get; set; }
        public int TotalPooledBookPages { get; set; }
        public int TotalMembers { get; set; }
        public int BookListingsPerPage { get; set; }
        public int MemberListingsPerPage { get; set; }
    }
}
