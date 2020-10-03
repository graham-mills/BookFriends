using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    [Table("CommunityGroup")]
    public class CommunityGroup
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public String Name { get; set; }
        public String Description { get; set; }
        public virtual ICollection<CommunityMember> CommunityMembers { get; set; }

        public CommunityGroup()
        {
            CommunityMembers = new List<CommunityMember>();
        }

        public object ToAnonymousDto()
        {
            return new
            {
                id = Id.ToString(),
                name = Name,
                description = Description,
                memberCount = CommunityMembers.Count
            };
        }


    }
}
