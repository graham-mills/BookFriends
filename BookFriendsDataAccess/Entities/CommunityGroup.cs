using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    [Table("CommunityGroup")]
    public class CommunityGroup : ISearchQueryableEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public String Name { get; set; }
        public String Description { get; set; }
        public String Keywords { get; set; }
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

        /// <summary>
        /// Returns selected string data that we want the
        /// entity to be discovered by when the user enters
        /// a search query.
        /// </summary>
        public IEnumerable<string> GetSearchQueryableStrings()
        {
            var queryableStrings = new List<string>();
            queryableStrings.Add(Name);
            Keywords.Split(',').ToList().ForEach(w => queryableStrings.Add(w.Trim()));
            return queryableStrings;
        }
    }
}
