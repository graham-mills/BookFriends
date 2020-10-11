using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriendsDataAccess.Entities
{
    [Table("PooledBook")]
    public class PooledBook : ISearchQueryableEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("OwnedBookId")]
        public virtual OwnedBook OwnedBook { get; set; }
        [Required]
        [ForeignKey("CommunityMemberId")]
        public virtual CommunityMember CommunityMember { get; set; }

        /// <summary>
        /// Returns selected string data that we want the
        /// entity to be discovered by when the user enters
        /// a search query.
        /// </summary>
        public IEnumerable<string> GetSearchQueryableStrings()
        {
            var queryableStrings = new List<string>();
            queryableStrings.Add(OwnedBook.Book.Name);
            queryableStrings.Add(OwnedBook.Book.Series);
            queryableStrings.Add(OwnedBook.Book.Publisher);
            foreach(var author in OwnedBook.Book.Authors)
            {
                queryableStrings.Add(author.Author.LastName);
                queryableStrings.AddRange(author.Author.FirstNames.Split(new char[','], StringSplitOptions.RemoveEmptyEntries));
            }
            return queryableStrings;
        }
    }
}
