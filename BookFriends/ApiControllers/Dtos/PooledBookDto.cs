using BookFriendsDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookFriends.ApiControllers.Dtos
{
    public class PooledBookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Series { get; set; }
        public string Notes { get; set; }
        public AuthorDto[] Authors { get; set; }
        public CommunityMemberDto Owner { get; set; }

        public PooledBookDto(PooledBook entity)
        {
            Id = entity.Id;
            Name = entity.OwnedBook.Book.Name;
            Publisher = entity.OwnedBook.Book.Publisher;
            Series = entity.OwnedBook.Book.Series;
            Notes = entity.OwnedBook.Notes;
            Authors = entity.OwnedBook.Book.Authors.Select(e => new AuthorDto(e.Author)).ToArray();
            Owner = new CommunityMemberDto(entity.CommunityMember);
        }
    }
}
