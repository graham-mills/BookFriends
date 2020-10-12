using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ApiControllers.Dtos
{
    public class CommunityMemberDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public CommunityMemberDto(CommunityMember entity)
        {
            Id = entity.Id;
            Name = entity.User.DisplayName;
        }
    }
}
