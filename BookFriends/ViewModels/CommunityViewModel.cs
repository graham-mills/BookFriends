using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ViewModels
{
    public class CommunityViewModel
    {
        public CommunityViewModel()
        {
            Memberships = new List<CommunityMember>();
            PooledBooks = new List<PooledBook>();
        }
        public CommunityGroup CommunityGroup { get; set; }
        public List<PooledBook> PooledBooks { get; set; }
        public List<CommunityMember> Memberships { get; set; }
        public int TotalPooledBooks { get; set; }
        public int TotalMembers { get; set; }
    }
}
