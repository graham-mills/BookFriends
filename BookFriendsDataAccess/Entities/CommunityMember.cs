using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    [Table("CommunityMember")]
    public class CommunityMember
    {   
        public enum MembershipRole
        {
            Member    = 0,
            Moderator = 1,
            Admin     = 2
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual CommunityGroup CommunityGroup { get; set; }
        [Required]
        public MembershipRole Role { get; set; }
        public virtual ICollection<PooledBook> PooledBooks { get; set; }

        public CommunityMember()
        {
            PooledBooks = new List<PooledBook>();
        }
    }
}
