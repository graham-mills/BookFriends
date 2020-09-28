using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    public class PooledBook
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public OwnedBook MemberBook { get; set; }
        [Required]
        public CommunityMember CommunityMember { get; set; }
    }
}
