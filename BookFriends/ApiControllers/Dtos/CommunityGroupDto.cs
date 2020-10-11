using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ApiControllers.Dtos
{
    public class CommunityGroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MemberCount { get; set; }
        
        public CommunityGroupDto(CommunityGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            MemberCount = entity.CommunityMembers.Count;
        }
    }
}
