using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    [Table("PooledBook")]
    public class PooledBook
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("OwnedBookId")]
        public virtual OwnedBook OwnedBook { get; set; }
        [Required]
        [ForeignKey("CommunityMemberId")]
        public virtual CommunityMember CommunityMember { get; set; }
    }
}
