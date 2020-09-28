using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    public class CommunityMember
    {   
        public enum MembershipRole
        {
            MEMBER    = 0,
            MODERATOR = 1,
            ADMIN     = 2,

            FIRST  = MEMBER,
            LAST   = ADMIN,
            LENGTH = LAST + 1
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public CommunityGroup CommunityGroup { get; set; }
        [Required]
        public MembershipRole Role { get; set; }
        public virtual ICollection<PooledBook> PooledBooks { get; set; }
    }
}
