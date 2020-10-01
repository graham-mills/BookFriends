using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public String EmailAddress { get; set; }
        [Required]
        [StringLength(maximumLength:30, MinimumLength = 1)]
        public String DisplayName { get; set; }
        public virtual ICollection<CommunityMember> Memberships { get; set; }
        public virtual ICollection<OwnedBook> OwnedBooks { get; set; }

        public User()
        {
            Memberships = new List<CommunityMember>();
            OwnedBooks = new List<OwnedBook>();
        }
    }
}
