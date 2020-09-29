using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    public class CommunityGroup
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual List<CommunityMember> CommunityMembers { get; set; }

        public CommunityGroup()
        {
            CommunityMembers = new List<CommunityMember>();
        }

        public int GetMemberCount() => CommunityMembers != null ? CommunityMembers.Count : 0;
    }
}
